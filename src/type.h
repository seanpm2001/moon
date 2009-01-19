/*
 * Automatically generated, do not edit this file directly
 */

/*
 * type.h: Generated code for the type system.
 *
 * Contact:
 *   Moonlight List (moonlight-list@lists.ximian.com)
 *
 * Copyright 2007 Novell, Inc. (http://www.novell.com)
 *
 * See the LICENSE file included with the distribution for details.
 * 
 */
#ifndef __TYPE_H__
#define __TYPE_H__

#include <glib.h>

class DependencyObject;
class Surface;
class Types;

typedef gint64 TimeSpan;
typedef DependencyObject *create_inst_func (void);

class Type {
public:
	enum Kind {
		// START_MANAGED_MAPPING
		INVALID,
				ANIMATION,
		ANIMATIONCLOCK,
		APPLICATION,// Silverlight 2.0 only
		ARCSEGMENT,
		ASFPACKET,
		ASFPARSER,
		ASSEMBLYPART,// Silverlight 2.0 only
		ASSEMBLYPART_COLLECTION,// Silverlight 2.0 only
		BEGINSTORYBOARD,
		BEZIERSEGMENT,
		BINDINGEXPRESSION,// Silverlight 2.0 only
		BINDINGEXPRESSIONBASE,// Silverlight 2.0 only
		BITMAPIMAGE,
		BOOL,
		BORDER,// Silverlight 2.0 only
		BRUSH,
		CANVAS,
		CHAR,
		CLOCK,
		CLOCKGROUP,
		COLLECTION,
		COLOR,
		COLORANIMATION,
		COLORANIMATIONUSINGKEYFRAMES,
		COLORKEYFRAME,
		COLORKEYFRAME_COLLECTION,
		COLUMNDEFINITION,// Silverlight 2.0 only
		COLUMNDEFINITION_COLLECTION,// Silverlight 2.0 only
		CONTENTCONTROL,// Silverlight 2.0 only
		CONTROL,// Silverlight 2.0 only
		CONTROLTEMPLATE,// Silverlight 2.0 only
		CORNERRADIUS,// Silverlight 2.0 only
		DATATEMPLATE,// Silverlight 2.0 only
		DEEPZOOMIMAGETILESOURCE,
		DEPENDENCY_OBJECT,
		DEPENDENCY_OBJECT_COLLECTION,
		DEPENDENCYPROPERTY,
		DEPLOYMENT,// Silverlight 2.0 only
		DISCRETECOLORKEYFRAME,
		DISCRETEDOUBLEKEYFRAME,
		DISCRETEOBJECTKEYFRAME,
		DISCRETEPOINTKEYFRAME,
		DISPATCHERTIMER,
		DOUBLE,
		DOUBLE_COLLECTION,
		DOUBLEANIMATION,
		DOUBLEANIMATIONUSINGKEYFRAMES,
		DOUBLEKEYFRAME,
		DOUBLEKEYFRAME_COLLECTION,
		DOWNLOADER,
		DRAWINGATTRIBUTES,
		DURATION,
		ELLIPSE,
		ELLIPSEGEOMETRY,
		ERROREVENTARGS,
		EVENTARGS,
		EVENTOBJECT,
		EVENTTRIGGER,
		EXPRESSION,// Silverlight 2.0 only
		FRAMEWORKELEMENT,
		FRAMEWORKTEMPLATE,// Silverlight 2.0 only
		GENERALTRANSFORM,
		GEOMETRY,
		GEOMETRY_COLLECTION,
		GEOMETRYGROUP,
		GLYPHS,
		GRADIENTBRUSH,
		GRADIENTSTOP,
		GRADIENTSTOP_COLLECTION,
		GRID,// Silverlight 2.0 only
		GRIDLENGTH,// Silverlight 2.0 only
		IIMAGECONVERTER,
		IMAGE,
		IMAGEBRUSH,
		IMAGEERROREVENTARGS,
		IMEDIADECODER,
		IMEDIADEMUXER,
		IMEDIAOBJECT,
		IMEDIASOURCE,
		IMEDIASTREAM,
		INKPRESENTER,
		INLINE,
		INLINE_COLLECTION,
		INPUTMETHOD,
		INT32,
		INT64,
		ITEM_COLLECTION,
		KEYEVENTARGS,
		KEYFRAME,
		KEYFRAME_COLLECTION,
		KEYSPLINE,
		KEYTIME,
		LAYOUTINFORMATION,
		LINE,
		LINEARCOLORKEYFRAME,
		LINEARDOUBLEKEYFRAME,
		LINEARGRADIENTBRUSH,
		LINEARPOINTKEYFRAME,
		LINEBREAK,
		LINEGEOMETRY,
		LINESEGMENT,
		MANAGED,// Silverlight 2.0 only
		MANAGEDTYPEINFO,
		MANUALTIMESOURCE,
		MARKERREACHEDEVENTARGS,
		MATRIX,
		MATRIXTRANSFORM,
		MEDIA,
		MEDIAATTRIBUTE,
		MEDIAATTRIBUTE_COLLECTION,
		MEDIABASE,
		MEDIAELEMENT,
		MEDIAERROREVENTARGS,
		MEDIAMARKER,
		MOUSEEVENTARGS,
		MULTISCALEIMAGE,// Silverlight 2.0 only
		MULTISCALESUBIMAGE,
		MULTISCALETILESOURCE,
		NAMESCOPE,
		NPOBJ,
		OBJECT,
		OBJECTANIMATIONUSINGKEYFRAMES,
		OBJECTKEYFRAME,
		OBJECTKEYFRAME_COLLECTION,
		PANEL,
		PARALLELTIMELINE,
		PARSERERROREVENTARGS,
		PASSWORDBOX,// Silverlight 2.0 only
		PATH,
		PATHFIGURE,
		PATHFIGURE_COLLECTION,
		PATHGEOMETRY,
		PATHSEGMENT,
		PATHSEGMENT_COLLECTION,
		POINT,
		POINT_COLLECTION,
		POINTANIMATION,
		POINTANIMATIONUSINGKEYFRAMES,
		POINTKEYFRAME,
		POINTKEYFRAME_COLLECTION,
		POLYBEZIERSEGMENT,
		POLYGON,
		POLYLINE,
		POLYLINESEGMENT,
		POLYQUADRATICBEZIERSEGMENT,
		POPUP,
		QUADRATICBEZIERSEGMENT,
		RADIALGRADIENTBRUSH,
		RECT,
		RECTANGLE,
		RECTANGLEGEOMETRY,
		REPEATBEHAVIOR,
		RESOURCE_DICTIONARY,
		ROTATETRANSFORM,
		ROUTEDEVENTARGS,
		ROWDEFINITION,// Silverlight 2.0 only
		ROWDEFINITION_COLLECTION,// Silverlight 2.0 only
		RUN,
		SCALETRANSFORM,
		SETTER,// Silverlight 2.0 only
		SETTERBASE,// Silverlight 2.0 only
		SETTERBASE_COLLECTION,// Silverlight 2.0 only
		SHAPE,
		SIZE,// Silverlight 2.0 only
		SIZECHANGEDEVENTARGS,// Silverlight 2.0 only
		SKEWTRANSFORM,
		SOLIDCOLORBRUSH,
		SPLINECOLORKEYFRAME,
		SPLINEDOUBLEKEYFRAME,
		SPLINEPOINTKEYFRAME,
		STORYBOARD,
		STRING,
		STROKE,
		STROKE_COLLECTION,
		STYLE,// Silverlight 2.0 only
		STYLUSINFO,
		STYLUSPOINT,
		STYLUSPOINT_COLLECTION,
		SURFACE,
		SYSTEMTIMESOURCE,
		TEXTBLOCK,
		TEXTBOX,// Silverlight 2.0 only
		TEXTBOXMODELCHANGEDEVENTARGS,// Silverlight 2.0 only
		TEXTBOXVIEW,// Silverlight 2.0 only
		TEXTCHANGEDEVENTARGS,// Silverlight 2.0 only
		THICKNESS,
		TILEBRUSH,
		TIMELINE,
		TIMELINE_COLLECTION,
		TIMELINEGROUP,
		TIMELINEMARKER,
		TIMELINEMARKER_COLLECTION,
		TIMEMANAGER,
		TIMESOURCE,
		TIMESPAN,
		TRANSFORM,
		TRANSFORM_COLLECTION,
		TRANSFORMGROUP,
		TRANSLATETRANSFORM,
		TRIGGER_COLLECTION,
		TRIGGERACTION,
		TRIGGERACTION_COLLECTION,
		TRIGGERBASE,
		UIELEMENT,
		UIELEMENT_COLLECTION,
		UINT32,
		UINT64,
		UNMANAGEDMATRIX,// Silverlight 2.0 only
		USERCONTROL,// Silverlight 2.0 only
		VIDEOBRUSH,
		VISUALBRUSH,

		LASTTYPE,
		// END_MANAGED_MAPPING
	};
	
	static Type *Find (const char *name);
	static Type *Find (Type::Kind type);
	static Type *Find (Types *additional_types, Type::Kind type);
	
	bool IsSubclassOf (Type::Kind super);
	static bool IsSubclassOf (Type::Kind type, Type::Kind super);

	bool IsSubclassOf (Types *additional_types, Type::Kind super);
	static bool IsSubclassOf (Types *additional_types, Type::Kind type, Type::Kind super);
	
	int LookupEvent (const char *event_name);
	const char *LookupEventName (int id);
	DependencyObject *CreateInstance ();
	const char *GetContentPropertyName ();
	
	Type::Kind GetKind () { return type; }
	Type::Kind GetParent () { return parent; }
	bool IsValueType () { return value_type; }
	const char *GetName () { return name; }
	int GetEventCount () { return total_event_count; }
	
	~Type ();
	
	Type *Clone ();
	
public: // private:
	Type::Kind type; // this type
	Type::Kind parent; // parent type, INVALID if no parent
	bool value_type; // if this type is a value type

	const char *name; // The name as it appears in code.
	const char *kindname; // The name as it appears in the Type::Kind enum.

	int event_count; // number of events in this class
	int total_event_count; // number of events in this class and all base classes
	const char **events; // the events this class has

	create_inst_func *create_inst; // a function pointer to create an instance of this type

	const char *content_property;
	GHashTable *properties; // Registered DependencyProperties for this type

	// Custom DependencyProperties for this type
	// The catch here is that SL allows us to register several DPs with the same name,
	// and when looking up DP on name they seem to return the latest DP registered
	// with that name.
	// So we keep one list of all registed DPs (custom_properties) in order to track
	// them all and free them upon shutdown, and a hash table to look up DP on name,
	// and if an entry already exists for a DP in the hash table, we overwrite it 
	// with the new DP.
	GHashTable *custom_properties_hash;
	GSList *custom_properties; 
};

extern Type type_infos [Type::LASTTYPE + 1];

class Types {
private:
	Type **types;
	int size; // The allocated size of the array
	int count; // The number of elements in the array (<= length)
	
	void EnsureSize (int size);
	// Note that we need to clone native types here too, since user code can 
	// register properties with native types.
	void CloneStaticTypes ();
	
public:
	/* @GenerateCBinding,GeneratePInvoke,Version=2.0 */
	Types ();
	/* @GenerateCBinding,GeneratePInvoke,Version=2.0 */
	~Types ();
	
	/* @GenerateCBinding,GeneratePInvoke,Version=2.0 */
	Type::Kind RegisterType (const char *name, void *gc_handle, Type::Kind parent);
	/* @GenerateCBinding,GeneratePInvoke,Version=2.0 */
	Type *Find (Type::Kind type);
};

G_BEGIN_DECLS

bool type_get_value_type (Type::Kind type);
DependencyObject *type_create_instance (Type *type);
DependencyObject *type_create_instance_from_kind (Type::Kind kind);

void types_init (void);
const char *type_get_name (Type::Kind type);
bool type_is_dependency_object (Type::Kind type);

/* @IncludeInKinds */
struct ManagedTypeInfo {
	char *assembly_name;
	char *full_name;
};

G_END_DECLS

#endif

