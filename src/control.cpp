/*
 * control.cpp:
 *
 * Author:
 *   Miguel de Icaza (miguel@novell.com)
 *
 * Copyright 2007 Novell, Inc. (http://www.novell.com)
 *
 * See the LICENSE file included with the distribution for details.
 * 
 */
#include <config.h>
#include <string.h>
#include <gtk/gtk.h>
#include <malloc.h>
#include <glib.h>
#include <stdlib.h>
#define Visual _XVisual
#include <cairo-xlib.h>
#undef Visual
#include "runtime.h"
#include "control.h"

void 
Control::UpdateTransform ()
{
	if (real_object){
		real_object->UpdateTransform ();
		absolute_xform = real_object->absolute_xform;
	}
}

void 
Control::Render (cairo_t *cr, int x, int y, int width, int height)
{
  if (!GetVisible ())
    printf ("wtf?\n");
	if (real_object)
		real_object->DoRender (cr, x, y, width, height);
}

Rect
Control::GetBounds ()
{
	if (real_object)
		return real_object->GetBounds();
	else
		return Rect (0,0,0,0);
}

void 
Control::ComputeBounds ()
{
	/* nothing to do here */
}

void 
Control::GetTransformFor (UIElement *item, cairo_matrix_t *result)
{
	if (parent != NULL){
		parent->GetTransformFor (this, result);
	} else {
		cairo_matrix_init_identity (result);
	}
}

Point 
Control::GetTransformOrigin ()
{
	if (real_object)
		return real_object->GetTransformOrigin ();
	else
		return Point (0, 0);
}

bool 
Control::InsideObject (cairo_t *cr, double x, double y)
{
	if (real_object)
		return real_object->InsideObject (cr, x, y);
	else
		return false;
}

void
Control::HandleMotion (cairo_t *cr, int state, double x, double y, MouseCursor *cursor)
{
	if (real_object)
		real_object->HandleMotion (cr, state, x, y, cursor);
	FrameworkElement::HandleMotion (cr, state, x, y, NULL);
}

void
Control::HandleButtonPress (cairo_t *cr, int state, double x, double y)
{
	if (real_object)
		real_object->HandleButtonPress (cr, state, x, y);
	FrameworkElement::HandleButtonPress (cr, state, x, y);
}

void
Control::HandleButtonRelease (cairo_t *cr, int state, double x, double y)
{
	if (real_object)
		real_object->HandleButtonRelease (cr, state, x, y);
	FrameworkElement::HandleButtonRelease (cr, state, x, y);
}

void 
Control::Enter (cairo_t *cr, int state, double x, double y)
{
	if (real_object){
		FrameworkElement::Enter (cr, state, x, y);
		real_object->Enter (cr, state, x, y);
	}
}

void 
Control::Leave ()
{
	if (real_object){
		real_object->Leave ();
		FrameworkElement::Leave ();
	}
}

void 
Control::OnPropertyChanged (DependencyProperty *prop)
{
	if (real_object)
		real_object->OnPropertyChanged (prop);
}

void 
Control::OnSubPropertyChanged (DependencyProperty *prop, DependencyProperty *subprop)
{
	NotifyAttacheesOfPropertyChange (subprop);
}

void
Control::SetValue (DependencyProperty *property, Value *value)
{
	FrameworkElement::SetValue (property, value);
	if (real_object)
		real_object->SetValue (property, value);
}

Value*
Control::GetValue (DependencyProperty *property)
{
	if (real_object)
		return real_object->GetValue (property);
	else
		return FrameworkElement::GetValue (property);
}

void
Control::OnLoaded ()
{
	if (real_object)
		real_object->OnLoaded ();

	FrameworkElement::OnLoaded ();
}

Control::~Control ()
{
	if (real_object)
		base_unref (real_object);
}

UIElement*
Control::InitializeFromXaml (const char *xaml,
			     Type::Kind *element_type)
{
	// No callback, figure out how this will work in the plugin to satisfy deps
	UIElement *element = xaml_create_from_str (xaml, false, element_type);
	if (element == NULL)
		return NULL;

	if (real_object){
		real_object->parent = NULL;
		base_unref (real_object);
	}

	real_object = (FrameworkElement *) element;
	real_object->parent = this;

	real_object->Attach (NULL, this);

	// sink the ref, we own this
	base_ref (real_object);

	return element;
}

UIElement* 
control_initialize_from_xaml (Control *control, const char *xaml,
			      Type::Kind *element_type)
{
	return control->InitializeFromXaml (xaml, element_type);
}

Control *
control_new (void)
{
	return new Control ();
}
