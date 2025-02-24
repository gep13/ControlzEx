#pragma warning disable 1591, 618, CA1815, CA1028, CA1008
#pragma warning disable SA1303 // Const field names should begin with upper-case letter
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1309 // Field names should not begin with underscore
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable SA1401 // Fields should be private
#pragma warning disable SA1602 // Enumeration items should be documented
namespace ControlzEx.Standard
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.ConstrainedExecution;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Security;
    using System.Text;
    using JetBrains.Annotations;
    using Microsoft.Win32.SafeHandles;

    // Some COM interfaces and Win32 structures are already declared in the framework.
    // Interesting ones to remember in System.Runtime.InteropServices.ComTypes are:
    using IStream = System.Runtime.InteropServices.ComTypes.IStream;

    #region Native Values

    internal static class Win32Value
    {
        public const uint MAX_PATH = 260;
        public const uint INFOTIPSIZE = 1024;
        public const int TRUE = 1;
        public const int FALSE = 0;
        public const uint sizeof_WCHAR = 2;
        public const uint sizeof_CHAR = 1;
        public const uint sizeof_BOOL = 4;
    }

    /// <summary>
    /// HIGHCONTRAST flags
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    public enum HCF
    {
        HIGHCONTRASTON = 0x00000001,
        AVAILABLE = 0x00000002,
        HOTKEYACTIVE = 0x00000004,
        CONFIRMHOTKEY = 0x00000008,
        HOTKEYSOUND = 0x00000010,
        INDICATOR = 0x00000020,
        HOTKEYAVAILABLE = 0x00000040,
    }

    internal enum DROPEFFECT
    {
        NONE = 0,
        COPY = 1,
        MOVE = 2,
        LINK = 4,
        SCROLL = unchecked((int)0x80000000),
    }

    /// <summary>
    /// DROPIMAGE_*
    /// </summary>
    internal enum DROPIMAGETYPE
    {
        INVALID = -1,
        NONE = 0,
        COPY = DROPEFFECT.COPY,
        MOVE = DROPEFFECT.MOVE,
        LINK = DROPEFFECT.LINK,
        LABEL = 6,
        WARNING = 7,
        // Windows 7 and later
        NOIMAGE = 8,
    }

    /// <summary>
    /// BITMAPINFOHEADER Compression type.  BI_*.
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum BI
    {
        RGB = 0,
    }

    /// <summary>
    /// CombingRgn flags.  RGN_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum RGN
    {
        /// <summary>
        /// Creates the intersection of the two combined regions.
        /// </summary>
        AND = 1,

        /// <summary>
        /// Creates the union of two combined regions.
        /// </summary>
        OR = 2,

        /// <summary>
        /// Creates the union of two combined regions except for any overlapping areas.
        /// </summary>
        XOR = 3,

        /// <summary>
        /// Combines the parts of hrgnSrc1 that are not part of hrgnSrc2.
        /// </summary>
        DIFF = 4,

        /// <summary>
        /// Creates a copy of the region identified by hrgnSrc1.
        /// </summary>
        COPY = 5,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum CombineRgnResult
    {
        ERROR = 0,
        NULLREGION = 1,
        SIMPLEREGION = 2,
        COMPLEXREGION = 3,
    }

    /// <summary>
    /// For IWebBrowser2.  OLECMDEXECOPT_*
    /// </summary>
    internal enum OLECMDEXECOPT
    {
        DODEFAULT = 0,
        PROMPTUSER = 1,
        DONTPROMPTUSER = 2,
        SHOWHELP = 3
    }

    /// <summary>
    /// For IWebBrowser2.  OLECMDF_*
    /// </summary>
    internal enum OLECMDF
    {
        SUPPORTED = 1,
        ENABLED = 2,
        LATCHED = 4,
        NINCHED = 8,
        INVISIBLE = 16,
        DEFHIDEONCTXTMENU = 32
    }

    /// <summary>
    /// For IWebBrowser2.  OLECMDID_*
    /// </summary>
    internal enum OLECMDID
    {
        OPEN = 1,
        NEW = 2,
        SAVE = 3,
        SAVEAS = 4,
        SAVECOPYAS = 5,
        PRINT = 6,
        PRINTPREVIEW = 7,
        PAGESETUP = 8,
        SPELL = 9,
        PROPERTIES = 10,
        CUT = 11,
        COPY = 12,
        PASTE = 13,
        PASTESPECIAL = 14,
        UNDO = 15,
        REDO = 16,
        SELECTALL = 17,
        CLEARSELECTION = 18,
        ZOOM = 19,
        GETZOOMRANGE = 20,
        UPDATECOMMANDS = 21,
        REFRESH = 22,
        STOP = 23,
        HIDETOOLBARS = 24,
        SETPROGRESSMAX = 25,
        SETPROGRESSPOS = 26,
        SETPROGRESSTEXT = 27,
        SETTITLE = 28,
        SETDOWNLOADSTATE = 29,
        STOPDOWNLOAD = 30,
        ONTOOLBARACTIVATED = 31,
        FIND = 32,
        DELETE = 33,
        HTTPEQUIV = 34,
        HTTPEQUIV_DONE = 35,
        ENABLE_INTERACTION = 36,
        ONUNLOAD = 37,
        PROPERTYBAG2 = 38,
        PREREFRESH = 39,
        SHOWSCRIPTERROR = 40,
        SHOWMESSAGE = 41,
        SHOWFIND = 42,
        SHOWPAGESETUP = 43,
        SHOWPRINT = 44,
        CLOSE = 45,
        ALLOWUILESSSAVEAS = 46,
        DONTDOWNLOADCSS = 47,
        UPDATEPAGESTATUS = 48,
        PRINT2 = 49,
        PRINTPREVIEW2 = 50,
        SETPRINTTEMPLATE = 51,
        GETPRINTTEMPLATE = 52,
        PAGEACTIONBLOCKED = 55,
        PAGEACTIONUIQUERY = 56,
        FOCUSVIEWCONTROLS = 57,
        FOCUSVIEWCONTROLSQUERY = 58,
        SHOWPAGEACTIONMENU = 59
    }

    /// <summary>
    /// DATAOBJ_GET_ITEM_FLAGS.  DOGIF_*.
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum DOGIF
    {
        DEFAULT = 0x0000,
        TRAVERSE_LINK = 0x0001,    // if the item is a link get the target
        NO_HDROP = 0x0002,    // don't fallback and use CF_HDROP clipboard format
        NO_URL = 0x0004,    // don't fallback and use URL clipboard format
        ONLY_IF_ONE = 0x0008,    // only return the item if there is one item in the array
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum DWM_SIT
    {
        None,
        DISPLAYFRAME = 1,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    public enum ErrorModes
    {
        /// <summary>Use the system default, which is to display all error dialog boxes.</summary>
        Default = 0x0,

        /// <summary>
        /// The system does not display the critical-error-handler message box. 
        /// Instead, the system sends the error to the calling process.
        /// </summary>
        FailCriticalErrors = 0x1,

        /// <summary>
        /// 64-bit Windows:  The system automatically fixes memory alignment faults and makes them 
        /// invisible to the application. It does this for the calling process and any descendant processes.
        /// After this value is set for a process, subsequent attempts to clear the value are ignored.
        /// </summary>
        NoGpFaultErrorBox = 0x2,

        /// <summary>
        /// The system does not display the general-protection-fault message box. 
        /// This flag should only be set by debugging applications that handle general 
        /// protection (GP) faults themselves with an exception handler.
        /// </summary>
        NoAlignmentFaultExcept = 0x4,

        /// <summary>
        /// The system does not display a message box when it fails to find a file. 
        /// Instead, the error is returned to the calling process.
        /// </summary>
        NoOpenFileErrorBox = 0x8000
    }

    /// <summary>
    /// Non-client hit test values, HT*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum HT
    {
        ERROR = -2,
        TRANSPARENT = -1,
        NOWHERE = 0,
        CLIENT = 1,
        CAPTION = 2,
        SYSMENU = 3,
        GROWBOX = 4,
        MENU = 5,
        HSCROLL = 6,
        VSCROLL = 7,
        MINBUTTON = 8,
        MAXBUTTON = 9,
        LEFT = 10,
        RIGHT = 11,
        TOP = 12,
        TOPLEFT = 13,
        TOPRIGHT = 14,
        BOTTOM = 15,
        BOTTOMLEFT = 16,
        BOTTOMRIGHT = 17,
        BORDER = 18,
        OBJECT = 19,
        CLOSE = 20,
        HELP = 21
    }

    /// <summary>
    /// GetClassLongPtr values, GCLP_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum GCLP
    {
        HBRBACKGROUND = -10,
        HICON = -14,
        STYLE = -26,
        HICONSM = -34
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindow
    /// </summary>
    public enum GW
    {
        HWNDFIRST = 0,
        HWNDLAST = 1,
        HWNDNEXT = 2,
        HWNDPREV = 3,
        OWNER = 4,
        CHILD = 5,
        ENABLEDPOPUP = 6
    }

    /// <summary>
    /// GetWindowLongPtr values, GWL_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum GWL
    {
        WNDPROC = -4,
        HINSTANCE = -6,
        HWNDPARENT = -8,
        STYLE = -16,
        EXSTYLE = -20,
        USERDATA = -21,
        ID = -12
    }

    /// <summary>
    /// SystemMetrics.  SM_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum SM
    {
        CXSCREEN = 0,
        CYSCREEN = 1,
        CXVSCROLL = 2,
        CYHSCROLL = 3,
        CYCAPTION = 4,
        CXBORDER = 5,
        CYBORDER = 6,
        CXFIXEDFRAME = 7,
        CYFIXEDFRAME = 8,
        CYVTHUMB = 9,
        CXHTHUMB = 10,
        CXICON = 11,
        CYICON = 12,
        CXCURSOR = 13,
        CYCURSOR = 14,
        CYMENU = 15,
        CXFULLSCREEN = 16,
        CYFULLSCREEN = 17,
        CYKANJIWINDOW = 18,
        MOUSEPRESENT = 19,
        CYVSCROLL = 20,
        CXHSCROLL = 21,
        DEBUG = 22,
        SWAPBUTTON = 23,
        CXMIN = 28,
        CYMIN = 29,
        CXSIZE = 30,
        CYSIZE = 31,
        CXFRAME = 32,
        CXSIZEFRAME = CXFRAME,
        CYFRAME = 33,
        CYSIZEFRAME = CYFRAME,
        CXMINTRACK = 34,
        CYMINTRACK = 35,
        CXDOUBLECLK = 36,
        CYDOUBLECLK = 37,
        CXICONSPACING = 38,
        CYICONSPACING = 39,
        MENUDROPALIGNMENT = 40,
        PENWINDOWS = 41,
        DBCSENABLED = 42,
        CMOUSEBUTTONS = 43,
        SECURE = 44,
        CXEDGE = 45,
        CYEDGE = 46,
        CXMINSPACING = 47,
        CYMINSPACING = 48,
        CXSMICON = 49,
        CYSMICON = 50,
        CYSMCAPTION = 51,
        CXSMSIZE = 52,
        CYSMSIZE = 53,
        CXMENUSIZE = 54,
        CYMENUSIZE = 55,
        ARRANGE = 56,
        CXMINIMIZED = 57,
        CYMINIMIZED = 58,
        CXMAXTRACK = 59,
        CYMAXTRACK = 60,
        CXMAXIMIZED = 61,
        CYMAXIMIZED = 62,
        NETWORK = 63,
        CLEANBOOT = 67,
        CXDRAG = 68,
        CYDRAG = 69,
        SHOWSOUNDS = 70,
        CXMENUCHECK = 71,
        CYMENUCHECK = 72,
        SLOWMACHINE = 73,
        MIDEASTENABLED = 74,
        MOUSEWHEELPRESENT = 75,
        XVIRTUALSCREEN = 76,
        YVIRTUALSCREEN = 77,
        CXVIRTUALSCREEN = 78,
        CYVIRTUALSCREEN = 79,
        CMONITORS = 80,
        SAMEDISPLAYFORMAT = 81,
        IMMENABLED = 82,
        CXFOCUSBORDER = 83,
        CYFOCUSBORDER = 84,
        TABLETPC = 86,
        MEDIACENTER = 87,
        REMOTESESSION = 0x1000,
        REMOTECONTROL = 0x2001,
    }

    /// <summary>
    /// SystemParameterInfo values, SPI_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum SPI
    {
        GETBEEP = 0x0001,
        SETBEEP = 0x0002,
        GETMOUSE = 0x0003,
        SETMOUSE = 0x0004,
        GETBORDER = 0x0005,
        SETBORDER = 0x0006,
        GETKEYBOARDSPEED = 0x000A,
        SETKEYBOARDSPEED = 0x000B,
        LANGDRIVER = 0x000C,
        ICONHORIZONTALSPACING = 0x000D,
        GETSCREENSAVETIMEOUT = 0x000E,
        SETSCREENSAVETIMEOUT = 0x000F,
        GETSCREENSAVEACTIVE = 0x0010,
        SETSCREENSAVEACTIVE = 0x0011,
        GETGRIDGRANULARITY = 0x0012,
        SETGRIDGRANULARITY = 0x0013,
        SETDESKWALLPAPER = 0x0014,
        SETDESKPATTERN = 0x0015,
        GETKEYBOARDDELAY = 0x0016,
        SETKEYBOARDDELAY = 0x0017,
        ICONVERTICALSPACING = 0x0018,
        GETICONTITLEWRAP = 0x0019,
        SETICONTITLEWRAP = 0x001A,
        GETMENUDROPALIGNMENT = 0x001B,
        SETMENUDROPALIGNMENT = 0x001C,
        SETDOUBLECLKWIDTH = 0x001D,
        SETDOUBLECLKHEIGHT = 0x001E,
        GETICONTITLELOGFONT = 0x001F,
        SETDOUBLECLICKTIME = 0x0020,
        SETMOUSEBUTTONSWAP = 0x0021,
        SETICONTITLELOGFONT = 0x0022,
        GETFASTTASKSWITCH = 0x0023,
        SETFASTTASKSWITCH = 0x0024,

        SETDRAGFULLWINDOWS = 0x0025,
        GETDRAGFULLWINDOWS = 0x0026,
        GETNONCLIENTMETRICS = 0x0029,
        SETNONCLIENTMETRICS = 0x002A,
        GETMINIMIZEDMETRICS = 0x002B,
        SETMINIMIZEDMETRICS = 0x002C,
        GETICONMETRICS = 0x002D,
        SETICONMETRICS = 0x002E,
        SETWORKAREA = 0x002F,
        GETWORKAREA = 0x0030,
        SETPENWINDOWS = 0x0031,
        GETHIGHCONTRAST = 0x0042,
        SETHIGHCONTRAST = 0x0043,
        GETKEYBOARDPREF = 0x0044,
        SETKEYBOARDPREF = 0x0045,
        GETSCREENREADER = 0x0046,
        SETSCREENREADER = 0x0047,
        GETANIMATION = 0x0048,
        SETANIMATION = 0x0049,
        GETFONTSMOOTHING = 0x004A,
        SETFONTSMOOTHING = 0x004B,
        SETDRAGWIDTH = 0x004C,
        SETDRAGHEIGHT = 0x004D,
        SETHANDHELD = 0x004E,
        GETLOWPOWERTIMEOUT = 0x004F,
        GETPOWEROFFTIMEOUT = 0x0050,
        SETLOWPOWERTIMEOUT = 0x0051,
        SETPOWEROFFTIMEOUT = 0x0052,
        GETLOWPOWERACTIVE = 0x0053,
        GETPOWEROFFACTIVE = 0x0054,
        SETLOWPOWERACTIVE = 0x0055,
        SETPOWEROFFACTIVE = 0x0056,
        SETCURSORS = 0x0057,
        SETICONS = 0x0058,
        GETDEFAULTINPUTLANG = 0x0059,
        SETDEFAULTINPUTLANG = 0x005A,
        SETLANGTOGGLE = 0x005B,
        GETWINDOWSEXTENSION = 0x005C,
        SETMOUSETRAILS = 0x005D,
        GETMOUSETRAILS = 0x005E,
        SETSCREENSAVERRUNNING = 0x0061,
        SCREENSAVERRUNNING = SETSCREENSAVERRUNNING,
        GETFILTERKEYS = 0x0032,
        SETFILTERKEYS = 0x0033,
        GETTOGGLEKEYS = 0x0034,
        SETTOGGLEKEYS = 0x0035,
        GETMOUSEKEYS = 0x0036,
        SETMOUSEKEYS = 0x0037,
        GETSHOWSOUNDS = 0x0038,
        SETSHOWSOUNDS = 0x0039,
        GETSTICKYKEYS = 0x003A,
        SETSTICKYKEYS = 0x003B,
        GETACCESSTIMEOUT = 0x003C,
        SETACCESSTIMEOUT = 0x003D,

        GETSERIALKEYS = 0x003E,
        SETSERIALKEYS = 0x003F,
        GETSOUNDSENTRY = 0x0040,
        SETSOUNDSENTRY = 0x0041,
        GETSNAPTODEFBUTTON = 0x005F,
        SETSNAPTODEFBUTTON = 0x0060,
        GETMOUSEHOVERWIDTH = 0x0062,
        SETMOUSEHOVERWIDTH = 0x0063,
        GETMOUSEHOVERHEIGHT = 0x0064,
        SETMOUSEHOVERHEIGHT = 0x0065,
        GETMOUSEHOVERTIME = 0x0066,
        SETMOUSEHOVERTIME = 0x0067,
        GETWHEELSCROLLLINES = 0x0068,
        SETWHEELSCROLLLINES = 0x0069,
        GETMENUSHOWDELAY = 0x006A,
        SETMENUSHOWDELAY = 0x006B,

        GETWHEELSCROLLCHARS = 0x006C,
        SETWHEELSCROLLCHARS = 0x006D,

        GETSHOWIMEUI = 0x006E,
        SETSHOWIMEUI = 0x006F,

        GETMOUSESPEED = 0x0070,
        SETMOUSESPEED = 0x0071,
        GETSCREENSAVERRUNNING = 0x0072,
        GETDESKWALLPAPER = 0x0073,

        GETAUDIODESCRIPTION = 0x0074,
        SETAUDIODESCRIPTION = 0x0075,

        GETSCREENSAVESECURE = 0x0076,
        SETSCREENSAVESECURE = 0x0077,

        GETHUNGAPPTIMEOUT = 0x0078,
        SETHUNGAPPTIMEOUT = 0x0079,
        GETWAITTOKILLTIMEOUT = 0x007A,
        SETWAITTOKILLTIMEOUT = 0x007B,
        GETWAITTOKILLSERVICETIMEOUT = 0x007C,
        SETWAITTOKILLSERVICETIMEOUT = 0x007D,
        GETMOUSEDOCKTHRESHOLD = 0x007E,
        SETMOUSEDOCKTHRESHOLD = 0x007F,
        GETPENDOCKTHRESHOLD = 0x0080,
        SETPENDOCKTHRESHOLD = 0x0081,
        GETWINARRANGING = 0x0082,
        SETWINARRANGING = 0x0083,
        GETMOUSEDRAGOUTTHRESHOLD = 0x0084,
        SETMOUSEDRAGOUTTHRESHOLD = 0x0085,
        GETPENDRAGOUTTHRESHOLD = 0x0086,
        SETPENDRAGOUTTHRESHOLD = 0x0087,
        GETMOUSESIDEMOVETHRESHOLD = 0x0088,
        SETMOUSESIDEMOVETHRESHOLD = 0x0089,
        GETPENSIDEMOVETHRESHOLD = 0x008A,
        SETPENSIDEMOVETHRESHOLD = 0x008B,
        GETDRAGFROMMAXIMIZE = 0x008C,
        SETDRAGFROMMAXIMIZE = 0x008D,
        GETSNAPSIZING = 0x008E,
        SETSNAPSIZING = 0x008F,
        GETDOCKMOVING = 0x0090,
        SETDOCKMOVING = 0x0091,

        GETACTIVEWINDOWTRACKING = 0x1000,
        SETACTIVEWINDOWTRACKING = 0x1001,
        GETMENUANIMATION = 0x1002,
        SETMENUANIMATION = 0x1003,
        GETCOMBOBOXANIMATION = 0x1004,
        SETCOMBOBOXANIMATION = 0x1005,
        GETLISTBOXSMOOTHSCROLLING = 0x1006,
        SETLISTBOXSMOOTHSCROLLING = 0x1007,
        GETGRADIENTCAPTIONS = 0x1008,
        SETGRADIENTCAPTIONS = 0x1009,
        GETKEYBOARDCUES = 0x100A,
        SETKEYBOARDCUES = 0x100B,
        GETMENUUNDERLINES = GETKEYBOARDCUES,
        SETMENUUNDERLINES = SETKEYBOARDCUES,
        GETACTIVEWNDTRKZORDER = 0x100C,
        SETACTIVEWNDTRKZORDER = 0x100D,
        GETHOTTRACKING = 0x100E,
        SETHOTTRACKING = 0x100F,
        GETMENUFADE = 0x1012,
        SETMENUFADE = 0x1013,
        GETSELECTIONFADE = 0x1014,
        SETSELECTIONFADE = 0x1015,
        GETTOOLTIPANIMATION = 0x1016,
        SETTOOLTIPANIMATION = 0x1017,
        GETTOOLTIPFADE = 0x1018,
        SETTOOLTIPFADE = 0x1019,
        GETCURSORSHADOW = 0x101A,
        SETCURSORSHADOW = 0x101B,
        GETMOUSESONAR = 0x101C,
        SETMOUSESONAR = 0x101D,
        GETMOUSECLICKLOCK = 0x101E,
        SETMOUSECLICKLOCK = 0x101F,
        GETMOUSEVANISH = 0x1020,
        SETMOUSEVANISH = 0x1021,
        GETFLATMENU = 0x1022,
        SETFLATMENU = 0x1023,
        GETDROPSHADOW = 0x1024,
        SETDROPSHADOW = 0x1025,
        GETBLOCKSENDINPUTRESETS = 0x1026,
        SETBLOCKSENDINPUTRESETS = 0x1027,

        GETUIEFFECTS = 0x103E,
        SETUIEFFECTS = 0x103F,

        GETDISABLEOVERLAPPEDCONTENT = 0x1040,
        SETDISABLEOVERLAPPEDCONTENT = 0x1041,
        GETCLIENTAREAANIMATION = 0x1042,
        SETCLIENTAREAANIMATION = 0x1043,
        GETCLEARTYPE = 0x1048,
        SETCLEARTYPE = 0x1049,
        GETSPEECHRECOGNITION = 0x104A,
        SETSPEECHRECOGNITION = 0x104B,

        GETFOREGROUNDLOCKTIMEOUT = 0x2000,
        SETFOREGROUNDLOCKTIMEOUT = 0x2001,
        GETACTIVEWNDTRKTIMEOUT = 0x2002,
        SETACTIVEWNDTRKTIMEOUT = 0x2003,
        GETFOREGROUNDFLASHCOUNT = 0x2004,
        SETFOREGROUNDFLASHCOUNT = 0x2005,
        GETCARETWIDTH = 0x2006,
        SETCARETWIDTH = 0x2007,

        GETMOUSECLICKLOCKTIME = 0x2008,
        SETMOUSECLICKLOCKTIME = 0x2009,
        GETFONTSMOOTHINGTYPE = 0x200A,
        SETFONTSMOOTHINGTYPE = 0x200B,

        GETFONTSMOOTHINGCONTRAST = 0x200C,
        SETFONTSMOOTHINGCONTRAST = 0x200D,

        GETFOCUSBORDERWIDTH = 0x200E,
        SETFOCUSBORDERWIDTH = 0x200F,
        GETFOCUSBORDERHEIGHT = 0x2010,
        SETFOCUSBORDERHEIGHT = 0x2011,

        GETFONTSMOOTHINGORIENTATION = 0x2012,
        SETFONTSMOOTHINGORIENTATION = 0x2013,

        GETMINIMUMHITRADIUS = 0x2014,
        SETMINIMUMHITRADIUS = 0x2015,
        GETMESSAGEDURATION = 0x2016,
        SETMESSAGEDURATION = 0x2017,
    }

    /// <summary>
    /// SystemParameterInfo flag values, SPIF_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    public enum SPIF
    {
        None = 0,
        UPDATEINIFILE = 0x01,
        SENDWININICHANGE = 0x02,
    }

    [Flags]
    internal enum STATE_SYSTEM
    {
        UNAVAILABLE = 0x00000001, // Disabled
        SELECTED = 0x00000002,
        FOCUSED = 0x00000004,
        PRESSED = 0x00000008,
        CHECKED = 0x00000010,
        MIXED = 0x00000020,  // 3-state checkbox or toolbar button
        INDETERMINATE = MIXED,
        READONLY = 0x00000040,
        HOTTRACKED = 0x00000080,
        DEFAULT = 0x00000100,
        EXPANDED = 0x00000200,
        COLLAPSED = 0x00000400,
        BUSY = 0x00000800,
        FLOATING = 0x00001000,  // Children "owned" not "contained" by parent
        MARQUEED = 0x00002000,
        ANIMATED = 0x00004000,
        INVISIBLE = 0x00008000,
        OFFSCREEN = 0x00010000,
        SIZEABLE = 0x00020000,
        MOVEABLE = 0x00040000,
        SELFVOICING = 0x00080000,
        FOCUSABLE = 0x00100000,
        SELECTABLE = 0x00200000,
        LINKED = 0x00400000,
        TRAVERSED = 0x00800000,
        MULTISELECTABLE = 0x01000000,  // Supports multiple selection
        EXTSELECTABLE = 0x02000000,  // Supports extended selection
        ALERT_LOW = 0x04000000,  // This information is of low priority
        ALERT_MEDIUM = 0x08000000,  // This information is of medium priority
        ALERT_HIGH = 0x10000000,  // This information is of high priority
        PROTECTED = 0x20000000,  // access to this is restricted
        VALID = 0x3FFFFFFF,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum StockObject : int
    {
        WHITE_BRUSH = 0,
        LTGRAY_BRUSH = 1,
        GRAY_BRUSH = 2,
        DKGRAY_BRUSH = 3,
        BLACK_BRUSH = 4,
        NULL_BRUSH = 5,
        HOLLOW_BRUSH = NULL_BRUSH,
        WHITE_PEN = 6,
        BLACK_PEN = 7,
        NULL_PEN = 8,
        SYSTEM_FONT = 13,
        DEFAULT_PALETTE = 15,
    }

    /// <summary>
    /// CS_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum CS : uint
    {
        VREDRAW = 0x0001,
        HREDRAW = 0x0002,
        DBLCLKS = 0x0008,
        OWNDC = 0x0020,
        CLASSDC = 0x0040,
        PARENTDC = 0x0080,
        NOCLOSE = 0x0200,
        SAVEBITS = 0x0800,
        BYTEALIGNCLIENT = 0x1000,
        BYTEALIGNWINDOW = 0x2000,
        GLOBALCLASS = 0x4000,
        IME = 0x00010000,
        DROPSHADOW = 0x00020000
    }

    /// <summary>
    /// WindowStyle values, WS_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum WS : uint
    {
        OVERLAPPED = 0x00000000,
        POPUP = 0x80000000,
        CHILD = 0x40000000,
        MINIMIZE = 0x20000000,
        VISIBLE = 0x10000000,
        DISABLED = 0x08000000,
        CLIPSIBLINGS = 0x04000000,
        CLIPCHILDREN = 0x02000000,
        MAXIMIZE = 0x01000000,
        BORDER = 0x00800000,
        DLGFRAME = 0x00400000,
        VSCROLL = 0x00200000,
        HSCROLL = 0x00100000,
        SYSMENU = 0x00080000,
        THICKFRAME = 0x00040000,
        GROUP = 0x00020000,
        TABSTOP = 0x00010000,

        MINIMIZEBOX = 0x00020000,
        MAXIMIZEBOX = 0x00010000,

        CAPTION = BORDER | DLGFRAME,
        TILED = OVERLAPPED,
        ICONIC = MINIMIZE,
        SIZEBOX = THICKFRAME,
        TILEDWINDOW = OVERLAPPEDWINDOW,

        OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX,
        POPUPWINDOW = POPUP | BORDER | SYSMENU,
        CHILDWINDOW = CHILD,
    }

    /// <summary>
    /// Window message values, WM_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum WM
    {
        NULL = 0x0000,
        CREATE = 0x0001,
        DESTROY = 0x0002,
        MOVE = 0x0003,
        SIZE = 0x0005,
        ACTIVATE = 0x0006,
        SETFOCUS = 0x0007,
        KILLFOCUS = 0x0008,
        ENABLE = 0x000A,
        SETREDRAW = 0x000B,
        SETTEXT = 0x000C,
        GETTEXT = 0x000D,
        GETTEXTLENGTH = 0x000E,
        PAINT = 0x000F,
        CLOSE = 0x0010,
        QUERYENDSESSION = 0x0011,
        QUIT = 0x0012,
        QUERYOPEN = 0x0013,
        ERASEBKGND = 0x0014,
        SYSCOLORCHANGE = 0x0015,
        SHOWWINDOW = 0x0018,
        CTLCOLOR = 0x0019,
        WININICHANGE = 0x001A,
        SETTINGCHANGE = 0x001A,
        ACTIVATEAPP = 0x001C,
        SETCURSOR = 0x0020,
        MOUSEACTIVATE = 0x0021,
        CHILDACTIVATE = 0x0022,
        QUEUESYNC = 0x0023,
        GETMINMAXINFO = 0x0024,

        WINDOWPOSCHANGING = 0x0046,
        WINDOWPOSCHANGED = 0x0047,

        CONTEXTMENU = 0x007B,
        STYLECHANGING = 0x007C,
        STYLECHANGED = 0x007D,
        DISPLAYCHANGE = 0x007E,
        GETICON = 0x007F,
        SETICON = 0x0080,
        NCCREATE = 0x0081,
        NCDESTROY = 0x0082,
        NCCALCSIZE = 0x0083,
        NCHITTEST = 0x0084,
        NCPAINT = 0x0085,
        NCACTIVATE = 0x0086,
        GETDLGCODE = 0x0087,
        SYNCPAINT = 0x0088,
        NCMOUSEMOVE = 0x00A0,
        NCLBUTTONDOWN = 0x00A1,
        NCLBUTTONUP = 0x00A2,
        NCLBUTTONDBLCLK = 0x00A3,
        NCRBUTTONDOWN = 0x00A4,
        NCRBUTTONUP = 0x00A5,
        NCRBUTTONDBLCLK = 0x00A6,
        NCMBUTTONDOWN = 0x00A7,
        NCMBUTTONUP = 0x00A8,
        NCMBUTTONDBLCLK = 0x00A9,
        NCXBUTTONDOWN = 0x00ab,
        NCXBUTTONDBLCLK = 0x00ad,

        SYSKEYDOWN = 0x0104,
        SYSKEYUP = 0x0105,
        SYSCHAR = 0x0106,
        SYSDEADCHAR = 0x0107,
        COMMAND = 0x0111,
        SYSCOMMAND = 0x0112,

        // These two messages aren't defined in winuser.h, but they are sent to windows
        // with captions. They appear to paint the window caption and frame.
        // Unfortunately if you override the standard non-client rendering as we do
        // with CustomFrameWindow, sometimes Windows (not deterministically
        // reproducibly but definitely frequently) will send these messages to the
        // window and paint the standard caption/title over the top of the custom one.
        // So we need to handle these messages in CustomFrameWindow to prevent this
        // from happening.
        NCUAHDRAWCAPTION = 0xAE,
        NCUAHDRAWFRAME = 0xAF,

        MOUSEMOVE = 0x0200,
        LBUTTONDOWN = 0x0201,
        LBUTTONUP = 0x0202,
        LBUTTONDBLCLK = 0x0203,
        RBUTTONDOWN = 0x0204,
        RBUTTONUP = 0x0205,
        RBUTTONDBLCLK = 0x0206,
        MBUTTONDOWN = 0x0207,
        MBUTTONUP = 0x0208,
        MBUTTONDBLCLK = 0x0209,
        MOUSEWHEEL = 0x020A,
        XBUTTONDOWN = 0x020B,
        XBUTTONUP = 0x020C,
        XBUTTONDBLCLK = 0x020D,
        MOUSEHWHEEL = 0x020E,
        PARENTNOTIFY = 0x0210,

        SIZING = 0x0214,

        CAPTURECHANGED = 0x0215,
        POWERBROADCAST = 0x0218,
        DEVICECHANGE = 0x0219,

        ENTERSIZEMOVE = 0x0231,
        EXITSIZEMOVE = 0x0232,

        IME_SETCONTEXT = 0x0281,
        IME_NOTIFY = 0x0282,
        IME_CONTROL = 0x0283,
        IME_COMPOSITIONFULL = 0x0284,
        IME_SELECT = 0x0285,
        IME_CHAR = 0x0286,
        IME_REQUEST = 0x0288,
        IME_KEYDOWN = 0x0290,
        IME_KEYUP = 0x0291,

        NCMOUSELEAVE = 0x02A2,
        MOUSELEAVE = 0x02A3,

        TABLET_DEFBASE = 0x02C0,
        //WM_TABLET_MAXOFFSET = 0x20,

        TABLET_ADDED = TABLET_DEFBASE + 8,
        TABLET_DELETED = TABLET_DEFBASE + 9,
        TABLET_FLICK = TABLET_DEFBASE + 11,
        TABLET_QUERYSYSTEMGESTURESTATUS = TABLET_DEFBASE + 12,

        DPICHANGED = 0x02E0,

        CUT = 0x0300,
        COPY = 0x0301,
        PASTE = 0x0302,
        CLEAR = 0x0303,
        UNDO = 0x0304,
        RENDERFORMAT = 0x0305,
        RENDERALLFORMATS = 0x0306,
        DESTROYCLIPBOARD = 0x0307,
        DRAWCLIPBOARD = 0x0308,
        PAINTCLIPBOARD = 0x0309,
        VSCROLLCLIPBOARD = 0x030A,
        SIZECLIPBOARD = 0x030B,
        ASKCBFORMATNAME = 0x030C,
        CHANGECBCHAIN = 0x030D,
        HSCROLLCLIPBOARD = 0x030E,
        QUERYNEWPALETTE = 0x030F,
        PALETTEISCHANGING = 0x0310,
        PALETTECHANGED = 0x0311,
        HOTKEY = 0x0312,
        PRINT = 0x0317,
        PRINTCLIENT = 0x0318,
        APPCOMMAND = 0x0319,
        THEMECHANGED = 0x031A,

        DWMCOMPOSITIONCHANGED = 0x031E,
        DWMNCRENDERINGCHANGED = 0x031F,
        DWMCOLORIZATIONCOLORCHANGED = 0x0320,
        DWMWINDOWMAXIMIZEDCHANGE = 0x0321,

        GETTITLEBARINFOEX = 0x033F,

        #region Windows 7
        DWMSENDICONICTHUMBNAIL = 0x0323,
        DWMSENDICONICLIVEPREVIEWBITMAP = 0x0326,
        #endregion

        USER = 0x0400,

        // This is the hard-coded message value used by WinForms for Shell_NotifyIcon.
        // It's relatively safe to reuse.
        TRAYMOUSEMESSAGE = 0x800, //WM_USER + 1024
        APP = 0x8000,
    }

    /// <summary>
    /// Window style extended values, WS_EX_*
    /// </summary>
    [Flags]
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [CLSCompliant(false)]
    public enum WS_EX : uint
    {
        None = 0,
        DLGMODALFRAME = 0x00000001,
        NOPARENTNOTIFY = 0x00000004,
        TOPMOST = 0x00000008,
        ACCEPTFILES = 0x00000010,
        TRANSPARENT = 0x00000020,
        MDICHILD = 0x00000040,
        TOOLWINDOW = 0x00000080,
        WINDOWEDGE = 0x00000100,
        CLIENTEDGE = 0x00000200,
        CONTEXTHELP = 0x00000400,
        RIGHT = 0x00001000,
        LEFT = 0x00000000,
        RTLREADING = 0x00002000,
        LTRREADING = 0x00000000,
        LEFTSCROLLBAR = 0x00004000,
        RIGHTSCROLLBAR = 0x00000000,
        CONTROLPARENT = 0x00010000,
        STATICEDGE = 0x00020000,
        APPWINDOW = 0x00040000,
        LAYERED = 0x00080000,
        NOINHERITLAYOUT = 0x00100000, // Disable inheritence of mirroring by children
        LAYOUTRTL = 0x00400000, // Right to left mirroring
        COMPOSITED = 0x02000000,
        NOACTIVATE = 0x08000000,
        OVERLAPPEDWINDOW = WINDOWEDGE | CLIENTEDGE,
        PALETTEWINDOW = WINDOWEDGE | TOOLWINDOW | TOPMOST,
    }

    /// <summary>
    /// GetDeviceCaps nIndex values.
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum DeviceCap
    {
        /// <summary>Number of bits per pixel
        /// </summary>
        BITSPIXEL = 12,

        /// <summary>
        /// Number of planes
        /// </summary>
        PLANES = 14,

        /// <summary>
        /// Logical pixels inch in X
        /// </summary>
        LOGPIXELSX = 88,

        /// <summary>
        /// Logical pixels inch in Y
        /// </summary>
        LOGPIXELSY = 90,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum FO : int
    {
        MOVE = 0x0001,
        COPY = 0x0002,
        DELETE = 0x0003,
        RENAME = 0x0004,
    }

    /// <summary>
    /// "FILEOP_FLAGS", FOF_*.
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [CLSCompliant(false)]
    public enum FOF : ushort
    {
        MULTIDESTFILES = 0x0001,
        CONFIRMMOUSE = 0x0002,
        SILENT = 0x0004,
        RENAMEONCOLLISION = 0x0008,
        NOCONFIRMATION = 0x0010,
        WANTMAPPINGHANDLE = 0x0020,
        ALLOWUNDO = 0x0040,
        FILESONLY = 0x0080,
        SIMPLEPROGRESS = 0x0100,
        NOCONFIRMMKDIR = 0x0200,
        NOERRORUI = 0x0400,
        NOCOPYSECURITYATTRIBS = 0x0800,
        NORECURSION = 0x1000,
        NO_CONNECTED_ELEMENTS = 0x2000,
        WANTNUKEWARNING = 0x4000,
        NORECURSEREPARSE = 0x8000,
    }

    /// <summary>
    /// EnableMenuItem uEnable values, MF_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum MF : uint
    {
        /// <summary>
        /// Possible return value for EnableMenuItem
        /// </summary>
        DOES_NOT_EXIST = unchecked((uint)-1),
        ENABLED = 0,
        BYCOMMAND = 0,
        GRAYED = 1,
        DISABLED = 2,
    }

    /// <summary>Specifies the type of visual style attribute to set on a window.</summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [CLSCompliant(false)]
    public enum WINDOWTHEMEATTRIBUTETYPE : uint
    {
        /// <summary>Non-client area window attributes will be set.</summary>
        WTA_NONCLIENT = 1,
    }

    /// <summary>
    /// DWMFLIP3DWINDOWPOLICY.  DWMFLIP3D_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum DWMFLIP3D
    {
        DEFAULT,
        EXCLUDEBELOW,
        EXCLUDEABOVE,
        //LAST
    }

    /// <summary>
    /// DWMNCRENDERINGPOLICY. DWMNCRP_*
    /// </summary>
    internal enum DWMNCRP
    {
        USEWINDOWSTYLE,
        DISABLED,
        ENABLED,
        //LAST
    }

    /// <summary>
    /// DWMWINDOWATTRIBUTE.  DWMWA_*
    /// </summary>
    internal enum DWMWA
    {
        NCRENDERING_ENABLED = 1,
        NCRENDERING_POLICY,
        TRANSITIONS_FORCEDISABLED,
        ALLOW_NCPAINT,
        CAPTION_BUTTON_BOUNDS,
        NONCLIENT_RTL_LAYOUT,
        FORCE_ICONIC_REPRESENTATION,
        FLIP3D_POLICY,
        EXTENDED_FRAME_BOUNDS,

        // New to Windows 7:

        HAS_ICONIC_BITMAP,
        DISALLOW_PEEK,
        EXCLUDED_FROM_PEEK,

        // LAST
    }

    /// <summary>
    /// WindowThemeNonClientAttributes
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum WTNCA : uint
    {
        /// <summary>Prevents the window caption from being drawn.</summary>
        NODRAWCAPTION = 0x00000001,

        /// <summary>Prevents the system icon from being drawn.</summary>
        NODRAWICON = 0x00000002,

        /// <summary>Prevents the system icon menu from appearing.</summary>
        NOSYSMENU = 0x00000004,

        /// <summary>Prevents mirroring of the question mark, even in right-to-left (RTL) layout.</summary>
        NOMIRRORHELP = 0x00000008,

        /// <summary> A mask that contains all the valid bits.</summary>
        VALIDBITS = NODRAWCAPTION | NODRAWICON | NOMIRRORHELP | NOSYSMENU,
    }

    /// <summary>
    /// SetWindowPos options
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum SWP : uint
    {
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        ASYNCWINDOWPOS = 0x4000,

        /// <summary>
        /// Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        DEFERERASE = 0x2000,

        /// <summary>
        /// Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        DRAWFRAME = 0x0020,

        /// <summary>
        /// Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        FRAMECHANGED = 0x0020,

        /// <summary>
        /// Hides the window.
        /// </summary>
        HIDEWINDOW = 0x0080,

        /// <summary>
        /// Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        NOACTIVATE = 0x0010,

        /// <summary>
        /// Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        NOCOPYBITS = 0x0100,

        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        NOMOVE = 0x0002,

        /// <summary>
        /// Does not change the owner window's position in the Z order.
        /// </summary>
        NOOWNERZORDER = 0x0200,

        /// <summary>
        /// Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        NOREDRAW = 0x0008,

        /// <summary>
        /// Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        NOREPOSITION = 0x0200,

        /// <summary>
        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        NOSENDCHANGING = 0x0400,

        /// <summary>
        /// Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        NOSIZE = 0x0001,

        /// <summary>
        /// Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        NOZORDER = 0x0004,

        /// <summary>
        /// Displays the window.
        /// </summary>
        SHOWWINDOW = 0x0040,

        TOPMOST = NOACTIVATE | NOOWNERZORDER | NOSIZE | NOMOVE | NOREDRAW | NOSENDCHANGING
    }

    /// <summary>
    /// ShowWindow options
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum SW
    {
        HIDE = 0,
        SHOWNORMAL = 1,
        NORMAL = 1,
        SHOWMINIMIZED = 2,
        SHOWMAXIMIZED = 3,
        MAXIMIZE = 3,
        SHOWNOACTIVATE = 4,
        SHOW = 5,
        MINIMIZE = 6,
        SHOWMINNOACTIVE = 7,
        SHOWNA = 8,
        RESTORE = 9,
        SHOWDEFAULT = 10,
        FORCEMINIMIZE = 11,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum SC
    {
        SIZE = 0xF000,
        MOVE = 0xF010,
        MOUSEMOVE = 0xF012,
        MINIMIZE = 0xF020,
        MAXIMIZE = 0xF030,
        NEXTWINDOW = 0xF040,
        PREVWINDOW = 0xF050,
        CLOSE = 0xF060,
        VSCROLL = 0xF070,
        HSCROLL = 0xF080,
        MOUSEMENU = 0xF090,
        KEYMENU = 0xF100,
        ARRANGE = 0xF110,
        RESTORE = 0xF120,
        TASKLIST = 0xF130,
        SCREENSAVE = 0xF140,
        HOTKEY = 0xF150,
        DEFAULT = 0xF160,
        MONITORPOWER = 0xF170,
        CONTEXTHELP = 0xF180,
        SEPARATOR = 0xF00F,

        /// <summary>
        /// SCF_ISSECURE
        /// </summary>
        F_ISSECURE = 0x00000001,
        ICON = MINIMIZE,
        ZOOM = MAXIMIZE,
    }

    /// <summary>
    /// GDI+ Status codes
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum Status
    {
        Ok = 0,
        GenericError = 1,
        InvalidParameter = 2,
        OutOfMemory = 3,
        ObjectBusy = 4,
        InsufficientBuffer = 5,
        NotImplemented = 6,
        Win32Error = 7,
        WrongState = 8,
        Aborted = 9,
        FileNotFound = 10,
        ValueOverflow = 11,
        AccessDenied = 12,
        UnknownImageFormat = 13,
        FontFamilyNotFound = 14,
        FontStyleNotFound = 15,
        NotTrueTypeFont = 16,
        UnsupportedGdiplusVersion = 17,
        GdiplusNotInitialized = 18,
        PropertyNotFound = 19,
        PropertyNotSupported = 20,
        ProfileNotFound = 21,
    }

    internal enum MOUSEEVENTF : int
    {
        //mouse event constants
        LEFTDOWN = 2,
        LEFTUP = 4
    }

    /// <summary>
    /// MSGFLT_*.  New in Vista.  Realiased in Windows 7.
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum MSGFLT
    {
        // Win7 versions of this enum:
        RESET = 0,
        ALLOW = 1,
        DISALLOW = 2,

        // Vista versions of this enum:
        // ADD = 1,
        // REMOVE = 2,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum MSGFLTINFO
    {
        NONE = 0,
        ALREADYALLOWED_FORWND = 1,
        ALREADYDISALLOWED_FORWND = 2,
        ALLOWED_HIGHER = 3,
    }

    internal enum INPUT_TYPE : uint
    {
        MOUSE = 0,
    }

    /// <summary>
    /// Shell_NotifyIcon messages.  NIM_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [CLSCompliant(false)]
    public enum NIM : uint
    {
        ADD = 0,
        MODIFY = 1,
        DELETE = 2,
        SETFOCUS = 3,
        SETVERSION = 4,
    }

    /// <summary>
    /// SHAddToRecentDocuments flags.  SHARD_*
    /// </summary>
    internal enum SHARD
    {
        PIDL = 0x00000001,
        PATHA = 0x00000002,
        PATHW = 0x00000003,
        APPIDINFO = 0x00000004, // indicates the data type is a pointer to a SHARDAPPIDINFO structure
        APPIDINFOIDLIST = 0x00000005, // indicates the data type is a pointer to a SHARDAPPIDINFOIDLIST structure
        LINK = 0x00000006, // indicates the data type is a pointer to an IShellLink instance
        APPIDINFOLINK = 0x00000007, // indicates the data type is a pointer to a SHARDAPPIDINFOLINK structure 
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    public enum SLGP
    {
        SHORTPATH = 0x1,
        UNCPRIORITY = 0x2,
        RAWPATH = 0x4
    }

    /// <summary>
    /// Shell_NotifyIcon flags.  NIF_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Flags]
    [CLSCompliant(false)]
    public enum NIF : uint
    {
        MESSAGE = 0x0001,
        ICON = 0x0002,
        TIP = 0x0004,
        STATE = 0x0008,
        INFO = 0x0010,
        GUID = 0x0020,

        /// <summary>
        /// Vista only.
        /// </summary>
        REALTIME = 0x0040,

        /// <summary>
        /// Vista only.
        /// </summary>
        SHOWTIP = 0x0080,

        XP_MASK = MESSAGE | ICON | STATE | INFO | GUID,
        VISTA_MASK = XP_MASK | REALTIME | SHOWTIP,
    }

    /// <summary>
    /// Shell_NotifyIcon info flags.  NIIF_*
    /// </summary>
    internal enum NIIF
    {
        NONE = 0x00000000,
        INFO = 0x00000001,
        WARNING = 0x00000002,
        ERROR = 0x00000003,

        /// <summary>XP SP2 and later.</summary>
        USER = 0x00000004,

        /// <summary>XP and later.</summary>
        NOSOUND = 0x00000010,

        /// <summary>Vista and later.</summary>
        LARGE_ICON = 0x00000020,

        /// <summary>Windows 7 and later</summary>
        NIIF_RESPECT_QUIET_TIME = 0x00000080,

        /// <summary>XP and later.  Native version called NIIF_ICON_MASK.</summary>
        XP_ICON_MASK = 0x0000000F,
    }

    /// <summary>
    /// AC_*
    /// </summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum AC : byte
    {
        SRC_OVER = 0,
        SRC_ALPHA = 1,
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public enum ULW
    {
        ALPHA = 2,
        COLORKEY = 1,
        OPAQUE = 4,
    }

    internal enum WVR
    {
        ALIGNTOP = 0x0010,
        ALIGNLEFT = 0x0020,
        ALIGNBOTTOM = 0x0040,
        ALIGNRIGHT = 0x0080,
        HREDRAW = 0x0100,
        VREDRAW = 0x0200,
        VALIDRECTS = 0x0400,
        REDRAW = HREDRAW | VREDRAW,
    }

    internal enum DSH
    {
        ALLOWDROPDESCRIPTIONTEXT = 1,
    }

    internal enum ABEdge
    {
        ABE_LEFT = 0,
        ABE_TOP = 1,
        ABE_RIGHT = 2,
        ABE_BOTTOM = 3
    }

    internal enum ABMsg
    {
        ABM_NEW = 0,
        ABM_REMOVE = 1,
        ABM_QUERYPOS = 2,
        ABM_SETPOS = 3,
        ABM_GETSTATE = 4,
        ABM_GETTASKBARPOS = 5,
        ABM_ACTIVATE = 6,
        ABM_GETAUTOHIDEBAR = 7,
        ABM_SETAUTOHIDEBAR = 8,
        ABM_WINDOWPOSCHANGED = 9,
        ABM_SETSTATE = 10
    }

    #endregion

    #region SafeHandles

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        [SecurityCritical]
        private SafeFindHandle()
            : base(true)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.FindClose(this.handle);
        }
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public sealed class SafeDC : SafeHandleZeroOrMinusOneIsInvalid
    {
        private static class NativeMethods
        {
            [DllImport("user32.dll")]
            public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            [DllImport("user32.dll")]
            public static extern SafeDC GetDC(IntPtr hwnd);

            // Weird legacy function, documentation is unclear about how to use it...
            [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
            public static extern SafeDC CreateDC([MarshalAs(UnmanagedType.LPWStr)] string lpszDriver, [MarshalAs(UnmanagedType.LPWStr)] string? lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);

            [DllImport("gdi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern SafeDC CreateCompatibleDC(IntPtr hdc);

            [DllImport("gdi32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeleteDC(IntPtr hdc);
        }

        private IntPtr? _hwnd;
        private bool _created;

#pragma warning disable CA1044
        public IntPtr Hwnd
        {
            set
            {
                Assert.NullableIsNull(this._hwnd);
                this._hwnd = value;
            }
        }
#pragma warning restore CA1044

        private SafeDC()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            if (this._created)
            {
                return NativeMethods.DeleteDC(this.handle);
            }

            if (!this._hwnd.HasValue || this._hwnd.Value == IntPtr.Zero)
            {
                return true;
            }

            return NativeMethods.ReleaseDC(this._hwnd.Value, this.handle) == 1;
        }

        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        After:
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        */
        public static SafeDC CreateDC(string deviceName)
        {
            SafeDC? dc = null;
            try
            {
                // Should this really be on the driver parameter?
                dc = NativeMethods.CreateDC(deviceName, null, IntPtr.Zero, IntPtr.Zero);
            }
            finally
            {
                if (dc is not null)
                {
                    dc._created = true;
                }
            }

            if (dc is null
                || dc.IsInvalid)
            {
                dc?.Dispose();
                throw new SystemException("Unable to create a device context from the specified device information.");
            }

            return dc;
        }

        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes"), SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        After:
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        */
        public static SafeDC CreateCompatibleDC(SafeDC hdc)
        {
            SafeDC? dc = null;
            try
            {
                IntPtr hPtr = IntPtr.Zero;
                if (hdc is not null)
                {
                    hPtr = hdc.handle;
                }

                dc = NativeMethods.CreateCompatibleDC(hPtr);
                if (dc is null)
                {
                    HRESULT.ThrowLastError();
                }
            }
            finally
            {
                if (dc is not null)
                {
                    dc._created = true;
                }
            }

            if (dc is null
                || dc.IsInvalid)
            {
                dc?.Dispose();
                throw new SystemException("Unable to create a device context from the specified device information.");
            }

            return dc;
        }

        public static SafeDC GetDC(IntPtr hwnd)
        {
            SafeDC? dc = null;
            try
            {
                dc = NativeMethods.GetDC(hwnd);
            }
            finally
            {
                if (dc is not null)
                {
                    dc.Hwnd = hwnd;
                }
            }

            if (dc is null
                || dc.IsInvalid)
            {
                // GetDC does not set the last error...
                HRESULT.E_FAIL.ThrowIfFailed();
            }

            return dc!;
        }

        public static SafeDC GetDesktop()
        {
            return GetDC(IntPtr.Zero);
        }

        // In method 'SafeDC.WrapDC(IntPtr)', object '<>g__initLocal0' is not disposed along all exception paths.
        // Call System.IDisposable.Dispose on object '<>g__initLocal0' before all references to it are out of scope.

        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                // Sure...
                [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        After:
                // Sure...
        */
        // Sure...
        public static SafeDC WrapDC(IntPtr hdc)
        {
            // This won't actually get released by the class, but it allows an IntPtr to be converted for signatures.
            return new SafeDC
            {
                handle = hdc,
                _created = false,
                _hwnd = IntPtr.Zero,
            };
        }
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public sealed class SafeHBITMAP : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeHBITMAP()
            : base(true)
        {
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return NativeMethods.DeleteObject(this.handle);
        }
    }

    internal sealed class SafeGdiplusStartupToken : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeGdiplusStartupToken(IntPtr ptr)
            : base(true)
        {
            this.handle = ptr;
        }

        protected override bool ReleaseHandle()
        {
            Status s = NativeMethods.GdiplusShutdown(this.handle);
            return s == Status.Ok;
        }

        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        After:
                [SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        */
        public static SafeGdiplusStartupToken Startup()
        {
            IntPtr unsafeHandle;
            StartupOutput output;
            Status s = NativeMethods.GdiplusStartup(out unsafeHandle, new StartupInput(), out output);
            if (s == Status.Ok)
            {
                SafeGdiplusStartupToken safeHandle = new SafeGdiplusStartupToken(unsafeHandle);
                return safeHandle;
            }

            throw new Exception("Unable to initialize GDI+");
        }
    }

    internal sealed class SafeConnectionPointCookie : SafeHandleZeroOrMinusOneIsInvalid
    {
        private IConnectionPoint? _cp;

        // handle holds the cookie value.

        public SafeConnectionPointCookie(IConnectionPointContainer target, object sink, Guid eventId)
            : base(true)
        {
            Verify.IsNotNull(target, "target");
            Verify.IsNotNull(sink, "sink");
            Verify.IsNotDefault(eventId, "eventId");

            this.handle = IntPtr.Zero;

            IConnectionPoint? cp = null;
            try
            {
                int dwCookie;
                target.FindConnectionPoint(ref eventId, out cp);
                cp!.Advise(sink, out dwCookie);
                if (dwCookie == 0)
                {
                    throw new InvalidOperationException("IConnectionPoint::Advise returned an invalid cookie.");
                }

                this.handle = new IntPtr(dwCookie);
                this._cp = cp;
                cp = null;
            }
            finally
            {
                Utility.SafeRelease(ref cp);
            }
        }

        public void Disconnect()
        {
            this.ReleaseHandle();
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                if (!this.IsInvalid)
                {
                    int dwCookie = this.handle.ToInt32();
                    this.handle = IntPtr.Zero;

                    Assert.IsNotNull(this._cp);
                    try
                    {
                        this._cp!.Unadvise(dwCookie);
                    }
                    finally
                    {
                        Utility.SafeRelease(ref this._cp);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    #endregion

    #region Native Types

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct BLENDFUNCTION
    {
        // Must be AC_SRC_OVER
        public AC BlendOp;
        // Must be 0.
        public byte BlendFlags;
        // Alpha transparency between 0 (transparent) - 255 (opaque)
        public byte SourceConstantAlpha;
        // Must be AC_SRC_ALPHA
        public AC AlphaFormat;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct HIGHCONTRAST
    {
        public int cbSize;
        public HCF dwFlags;
        //[MarshalAs(UnmanagedType.LPWStr, SizeConst=80)]
        //public String lpszDefaultScheme;
        public IntPtr lpszDefaultScheme;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RGBQUAD
    {
        public byte rgbBlue;
        public byte rgbGreen;
        public byte rgbRed;
        public byte rgbReserved;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public BI biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFO
    {
        public BITMAPINFOHEADER bmiHeader;
        public RGBQUAD bmiColors;
    }

    // Win7 only.
    [StructLayout(LayoutKind.Sequential)]
    internal struct CHANGEFILTERSTRUCT
    {
        public uint cbSize;
        public MSGFLTINFO ExtStatus;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct CREATESTRUCT
    {
        public IntPtr lpCreateParams;
        public IntPtr hInstance;
        public IntPtr hMenu;
        public IntPtr hwndParent;
        public int cy;
        public int cx;
        public int y;
        public int x;
        public WS style;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClass;
        public WS_EX dwExStyle;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct SHFILEOPSTRUCT
    {
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.U4)]
        public FO wFunc;
        // double-null terminated arrays of LPWSTRS
        public string pFrom;
        public string pTo;
        [CLSCompliant(false)]
        [MarshalAs(UnmanagedType.U2)]
        public FOF fFlags;
        [MarshalAs(UnmanagedType.Bool)]
        public int fAnyOperationsAborted;
        public IntPtr hNameMappings;
        public string lpszProgressTitle;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct TITLEBARINFO
    {
        public int cbSize;
        public RECT rcTitleBar;
        public STATE_SYSTEM rgstate_TitleBar;
        public STATE_SYSTEM rgstate_Reserved;
        public STATE_SYSTEM rgstate_MinimizeButton;
        public STATE_SYSTEM rgstate_MaximizeButton;
        public STATE_SYSTEM rgstate_HelpButton;
        public STATE_SYSTEM rgstate_CloseButton;
    }

    // New to Vista.
    [StructLayout(LayoutKind.Sequential)]
    internal struct TITLEBARINFOEX
    {
        public int cbSize;
        public RECT rcTitleBar;
        public STATE_SYSTEM rgstate_TitleBar;
        public STATE_SYSTEM rgstate_Reserved;
        public STATE_SYSTEM rgstate_MinimizeButton;
        public STATE_SYSTEM rgstate_MaximizeButton;
        public STATE_SYSTEM rgstate_HelpButton;
        public STATE_SYSTEM rgstate_CloseButton;
        public RECT rgrect_TitleBar;
        public RECT rgrect_Reserved;
        public RECT rgrect_MinimizeButton;
        public RECT rgrect_MaximizeButton;
        public RECT rgrect_HelpButton;
        public RECT rgrect_CloseButton;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public class NOTIFYICONDATA
    {
        public int cbSize;
        public IntPtr hWnd;
        public int uID;
        [CLSCompliant(false)]
        public NIF uFlags;
        public int uCallbackMessage;
        public IntPtr hIcon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public char[] szTip = new char[128];

        /// <summary>
        /// The state of the icon.  There are two flags that can be set independently.
        /// NIS_HIDDEN = 1.  The icon is hidden.
        /// NIS_SHAREDICON = 2.  The icon is shared.
        /// </summary>
        [CLSCompliant(false)]
        public uint dwState;
        [CLSCompliant(false)]
        public uint dwStateMask;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public char[] szInfo = new char[256];
        // Prior to Vista this was a union of uTimeout and uVersion.  As of Vista, uTimeout has been deprecated.
        [CLSCompliant(false)]
        public uint uVersion;  // Used with Shell_NotifyIcon flag NIM_SETVERSION.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public char[] szInfoTitle = new char[64];
        [CLSCompliant(false)]
        public uint dwInfoFlags;
        public Guid guidItem;
        // Vista only
        private IntPtr hBalloonIcon;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LOGFONT
    {
        public int lfHeight;
        public int lfWidth;
        public int lfEscapement;
        public int lfOrientation;
        public int lfWeight;
        public byte lfItalic;
        public byte lfUnderline;
        public byte lfStrikeOut;
        public byte lfCharSet;
        public byte lfOutPrecision;
        public byte lfClipPrecision;
        public byte lfQuality;
        public byte lfPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string lfFaceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct NONCLIENTMETRICS
    {
        public int cbSize;
        public int iBorderWidth;
        public int iScrollWidth;
        public int iScrollHeight;
        public int iCaptionWidth;
        public int iCaptionHeight;
        public LOGFONT lfCaptionFont;
        public int iSmCaptionWidth;
        public int iSmCaptionHeight;
        public LOGFONT lfSmCaptionFont;
        public int iMenuWidth;
        public int iMenuHeight;
        public LOGFONT lfMenuFont;
        public LOGFONT lfStatusFont;
        public LOGFONT lfMessageFont;
        // Vista only
        public int iPaddedBorderWidth;

        public static NONCLIENTMETRICS VistaMetricsStruct
        {
            get
            {
                var ncm = default(NONCLIENTMETRICS);
                ncm.cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS));
                return ncm;
            }
        }

        public static NONCLIENTMETRICS XPMetricsStruct
        {
            get
            {
                var ncm = default(NONCLIENTMETRICS);
                // Account for the missing iPaddedBorderWidth
                ncm.cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS)) - sizeof(int);
                return ncm;
            }
        }
    }

    /// <summary>Defines options that are used to set window visual style attributes.</summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Explicit)]
    public struct WTA_OPTIONS
    {
        // public static readonly uint Size = (uint)Marshal.SizeOf(typeof(WTA_OPTIONS));
        [CLSCompliant(false)]
        public const uint Size = 8;

        /// <summary>
        /// A combination of flags that modify window visual style attributes.
        /// Can be a combination of the WTNCA constants.
        /// </summary>
        [FieldOffset(0)]
        [CLSCompliant(false)]
        public WTNCA dwFlags;

        /// <summary>
        /// A bitmask that describes how the values specified in dwFlags should be applied.
        /// If the bit corresponding to a value in dwFlags is 0, that flag will be removed.
        /// If the bit is 1, the flag will be added.
        /// </summary>
        [FieldOffset(4)]
        [CLSCompliant(false)]
        public WTNCA dwMask;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        /// <summary>Width of left border that retains its size.</summary>
        public int cxLeftWidth;

        /// <summary>Width of right border that retains its size.</summary>
        public int cxRightWidth;

        /// <summary>Height of top border that retains its size.</summary>
        public int cyTopHeight;

        /// <summary>Height of bottom border that retains its size.</summary>
        public int cyBottomHeight;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        private int _x;
        private int _y;

        public POINT(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public int X
        {
            get { return this._x; }
            set { this._x = value; }
        }

        public int Y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        public override bool Equals(object? obj)
        {
            if (obj is POINT)
            {
                var point = (POINT)obj;

                return point._x == this._x && point._y == this._y;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this._x.GetHashCode() ^ this._y.GetHashCode();
        }

        public static bool operator ==(POINT a, POINT b)
        {
            return a._x == b._x && a._y == b._y;
        }

        public static bool operator !=(POINT a, POINT b)
        {
            return !(a == b);
        }
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public class RefPOINT
    {
        public int x;
        public int y;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    [Serializable]
    public struct RECT : IEquatable<RECT>
    {
        public static readonly RECT Empty = default(RECT);

        public RECT(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

        public RECT(RECT rcSrc)
        {
            this.Left = rcSrc.Left;
            this.Top = rcSrc.Top;
            this.Right = rcSrc.Right;
            this.Bottom = rcSrc.Bottom;
        }

        public void Offset(int dx, int dy)
        {
            this.Left += dx;
            this.Top += dy;
            this.Right += dx;
            this.Bottom += dy;
        }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public int Width => this.Right - this.Left;

        public int Height => this.Bottom - this.Top;

        public static RECT Union(RECT rect1, RECT rect2)
        {
            return new RECT
            {
                Left = Math.Min(rect1.Left, rect2.Left),
                Top = Math.Min(rect1.Top, rect2.Top),
                Right = Math.Max(rect1.Right, rect2.Right),
                Bottom = Math.Max(rect1.Bottom, rect2.Bottom),
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is RECT rect && this.Equals(rect);
        }

        public bool Equals(RECT other)
        {
            return other.Bottom == this.Bottom
                   && other.Left == this.Left
                   && other.Right == this.Right
                   && other.Top == this.Top;
        }

        public bool IsEmpty =>
            // BUG : On Bidi OS (hebrew arabic) left > right
            this.Left >= this.Right || this.Top >= this.Bottom;

        public override string ToString()
        {
            if (this == Empty)
            {
                return "RECT {Empty}";
            }

            return "RECT { left : " + this.Left + " / top : " + this.Top + " / right : " + this.Right + " / bottom : " + this.Bottom + " }";
        }

        public override int GetHashCode()
        {
            if (this.IsEmpty)
            {
                return 0;
            }
            else
            {
                return (this.Left << 16 | Utility.LOWORD(this.Right)) ^ (this.Top << 16 | Utility.LOWORD(this.Bottom));
            }
        }

        public static bool operator ==(RECT rect1, RECT rect2)
        {
            return rect1.Equals(rect2);
        }

        public static bool operator !=(RECT rect1, RECT rect2)
        {
            return !rect1.Equals(rect2);
        }
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct StartupOutput
    {
        public IntPtr hook;
        public IntPtr unhook;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public class StartupInput
    {
        public int GdiplusVersion = 1;
        public IntPtr DebugEventCallback;
        public bool SuppressBackgroundThread;
        public bool SuppressExternalCodecs;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    [BestFitMapping(false)]
    public class WIN32_FIND_DATAW
    {
        public FileAttributes dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public int nFileSizeHigh;
        public int nFileSizeLow;
        public int dwReserved0;
        public int dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string? cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string? cAlternateFileName;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public class WINDOWPLACEMENT
    {
        public int length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));
        public int flags;
        public SW showCmd;
        public POINT minPosition;
        public POINT maxPosition;
        public RECT normalPosition;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS : IEquatable<WINDOWPOS>
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        [CLSCompliant(false)]
        public SWP flags;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"x: {this.x}; y: {this.y}; cx: {this.cx}; cy: {this.cy}; flags: {this.flags}";
        }

        public bool SizeAndPositionEquals(WINDOWPOS other)
        {
            return this.x == other.x
                   && this.y == other.y
                   && this.cx == other.cx
                   && this.cy == other.cy;
        }

        public bool IsEmpty()
        {
            return this.x == 0
                   && this.y == 0
                   && this.cx == 0
                   && this.cy == 0;
        }

        public bool Equals(WINDOWPOS other)
        {
            return this.hwnd.Equals(other.hwnd)
                   && this.hwndInsertAfter.Equals(other.hwndInsertAfter)
                   && this.x == other.x
                   && this.y == other.y
                   && this.cx == other.cx
                   && this.cy == other.cy
                   && this.flags == other.flags;
        }

        public override bool Equals(object? obj)
        {
            return obj is WINDOWPOS other
                   && this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.hwnd.GetHashCode();
                hashCode = (hashCode * 397) ^ this.hwndInsertAfter.GetHashCode();
                hashCode = (hashCode * 397) ^ this.x;
                hashCode = (hashCode * 397) ^ this.y;
                hashCode = (hashCode * 397) ^ this.cx;
                hashCode = (hashCode * 397) ^ this.cy;
                hashCode = (hashCode * 397) ^ (int)this.flags;
                return hashCode;
            }
        }

        public static bool operator ==(WINDOWPOS left, WINDOWPOS right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WINDOWPOS left, WINDOWPOS right)
        {
            return !left.Equals(right);
        }
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WNDCLASSEX
    {
        public int cbSize;
        [CLSCompliant(false)]
        public CS style;
        public WndProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszMenuName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszClassName;
        public IntPtr hIconSm;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public int cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public int dwStyle;
        public int dwExStyle;
        [CLSCompliant(false)]
        public uint dwWindowStatus;
        [CLSCompliant(false)]
        public uint cxWindowBorders;
        [CLSCompliant(false)]
        public uint cyWindowBorders;
        [CLSCompliant(false)]
        public ushort atomWindowType;
        [CLSCompliant(false)]
        public ushort wCreatorVersion;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public int mouseData;
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        [CLSCompliant(false)]
        public uint type;
        public MOUSEINPUT mi;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    [CLSCompliant(false)]
    public struct UNSIGNED_RATIO
    {
        public uint uiNumerator;
        public uint uiDenominator;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [CLSCompliant(false)]
    public struct DWM_TIMING_INFO
    {
        public int cbSize;
        public UNSIGNED_RATIO rateRefresh;
        public ulong qpcRefreshPeriod;
        public UNSIGNED_RATIO rateCompose;
        public ulong qpcVBlank;
        public ulong cRefresh;
        public uint cDXRefresh;
        public ulong qpcCompose;
        public ulong cFrame;
        public uint cDXPresent;
        public ulong cRefreshFrame;
        public ulong cFrameSubmitted;
        public uint cDXPresentSubmitted;
        public ulong cFrameConfirmed;
        public uint cDXPresentConfirmed;
        public ulong cRefreshConfirmed;
        public uint cDXRefreshConfirmed;
        public ulong cFramesLate;
        public uint cFramesOutstanding;
        public ulong cFrameDisplayed;
        public ulong qpcFrameDisplayed;
        public ulong cRefreshFrameDisplayed;
        public ulong cFrameComplete;
        public ulong qpcFrameComplete;
        public ulong cFramePending;
        public ulong qpcFramePending;
        public ulong cFramesDisplayed;
        public ulong cFramesComplete;
        public ulong cFramesPending;
        public ulong cFramesAvailable;
        public ulong cFramesDropped;
        public ulong cFramesMissed;
        public ulong cRefreshNextDisplayed;
        public ulong cRefreshNextPresented;
        public ulong cRefreshesDisplayed;
        public ulong cRefreshesPresented;
        public ulong cRefreshStarted;
        public ulong cPixelsReceived;
        public ulong cPixelsDrawn;
        public ulong cBuffersEmpty;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct SHDRAGIMAGE
    {
        public SIZE sizeDragImage;
        public POINT ptOffset;
        public IntPtr hbmpDragImage;
        public int crColorKey;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 1044)]
    internal struct DROPDESCRIPTION
    {
        public DROPIMAGETYPE type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szMessage;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szInsert;
    }

    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    [StructLayout(LayoutKind.Sequential)]
    public struct APPBARDATA
    {
        /// <summary>
        /// initialize this field using: Marshal.SizeOf(typeof(APPBARDATA));
        /// </summary>
        public int cbSize;
        public IntPtr hWnd;
        public int uCallbackMessage;
        public int uEdge;
        public RECT rc;
        public bool lParam;
    }

    #endregion

    /// <summary>Delegate declaration that matches native WndProc signatures.</summary>
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public delegate IntPtr WndProc(IntPtr hwnd, WM uMsg, IntPtr wParam, IntPtr lParam);

    /// <summary>Delegate declaration that matches managed WndProc signatures.</summary>
    internal delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);

    // Some native methods are shimmed through public versions that handle converting failures into thrown exceptions.
    [Obsolete(ControlzEx.DesignerConstants.Win32ElementWarning)]
    public static class NativeMethods
    {
        [DllImport("user32.dll", EntryPoint = "AdjustWindowRectEx", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _AdjustWindowRectEx(ref RECT lpRect, WS dwStyle, [MarshalAs(UnmanagedType.Bool)] bool bMenu, WS_EX dwExStyle);

        [CLSCompliant(false)]
        public static RECT AdjustWindowRectEx(RECT lpRect, WS dwStyle, bool bMenu, WS_EX dwExStyle)
        {
            // Native version modifies the parameter in place.
            if (!_AdjustWindowRectEx(ref lpRect, dwStyle, bMenu, dwExStyle))
            {
                HRESULT.ThrowLastError();
            }

            return lpRect;
        }

        [DllImport("user32.dll", EntryPoint = "AllowSetForegroundWindow", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _AllowSetForegroundWindow(int dwProcessId);

        public static void AllowSetForegroundWindow()
        {
            int ASFW_ANY = -1;
            AllowSetForegroundWindow(ASFW_ANY);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void AllowSetForegroundWindow(int dwProcessId)
        {
            if (!_AllowSetForegroundWindow(dwProcessId))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("user32.dll", EntryPoint = "ChangeWindowMessageFilter", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _ChangeWindowMessageFilter(WM message, MSGFLT dwFlag);

        [DllImport("user32.dll", EntryPoint = "ChangeWindowMessageFilterEx", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _ChangeWindowMessageFilterEx(IntPtr hwnd, WM message, MSGFLT action, [In, Out, Optional] ref CHANGEFILTERSTRUCT pChangeFilterStruct);

        // Note that processes at or below SECURITY_MANDATORY_LOW_RID are not allowed to change the message filter.
        // If those processes call this function, it will fail and generate the extended error code, ERROR_ACCESS_DENIED.
        public static HRESULT ChangeWindowMessageFilterEx(IntPtr hwnd, WM message, MSGFLT action, out MSGFLTINFO filterInfo)
        {
            filterInfo = MSGFLTINFO.NONE;

            bool ret;

            // This origins of this API were added for Vista.  The Ex version was added for Windows 7.
            // If we're not on either, then this message filter isolation doesn't exist.
            if (!Utility.IsOSVistaOrNewer)
            {
                return HRESULT.S_FALSE;
            }

            // If we're on Vista rather than Win7 then we can't use the Ex version of this function.
            // The Ex version is preferred if possible because this results in process-wide modifications of the filter
            // and is deprecated as of Win7.
            if (!Utility.IsOSWindows7OrNewer)
            {
                // Note that the Win7 MSGFLT_ALLOW/DISALLOW enum values map to the Vista MSGFLT_ADD/REMOVE
                ret = _ChangeWindowMessageFilter(message, action);
                if (!ret)
                {
                    return (HRESULT)Win32Error.GetLastError();
                }

                return HRESULT.S_OK;
            }

            var filterstruct = new CHANGEFILTERSTRUCT { cbSize = (uint)Marshal.SizeOf(typeof(CHANGEFILTERSTRUCT)) };
            ret = _ChangeWindowMessageFilterEx(hwnd, message, action, ref filterstruct);
            if (!ret)
            {
                return (HRESULT)Win32Error.GetLastError();
            }

            filterInfo = filterstruct.ExtStatus;
            return HRESULT.S_OK;
        }

        [DllImport("user32.dll", CharSet = CharSet.None, SetLastError = true, EntryPoint = "ClientToScreen")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [DllImport("user32.dll", CharSet = CharSet.None, SetLastError = true, EntryPoint = "ScreenToClient")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT point);

        [DllImport("gdi32.dll")]
        public static extern CombineRgnResult CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, RGN fnCombineMode);

        [DllImport("shell32.dll", EntryPoint = "CommandLineToArgvW", CharSet = CharSet.Unicode)]
        private static extern IntPtr _CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string cmdLine, out int numArgs);

        public static string[] CommandLineToArgvW(string cmdLine)
        {
            IntPtr argv = IntPtr.Zero;
            try
            {
                int numArgs = 0;

                argv = _CommandLineToArgvW(cmdLine, out numArgs);
                if (argv == IntPtr.Zero)
                {
                    throw new Win32Exception();
                }

                var result = new string[numArgs];

                for (int i = 0; i < numArgs; i++)
                {
                    IntPtr currArg = Marshal.ReadIntPtr(argv, i * Marshal.SizeOf(typeof(IntPtr)));
                    result[i] = Marshal.PtrToStringUni(currArg) ?? string.Empty;
                }

                return result;
            }
            finally
            {
                IntPtr p = _LocalFree(argv);
                // Otherwise LocalFree failed.
                Assert.AreEqual(IntPtr.Zero, p);
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", SetLastError = true)]
        private static extern SafeHBITMAP _CreateDIBSection(SafeDC hdc, [In] ref BITMAPINFO bitmapInfo, int iUsage, [Out] out IntPtr ppvBits, IntPtr hSection, int dwOffset);

        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection", SetLastError = true)]
        private static extern SafeHBITMAP _CreateDIBSectionIntPtr(IntPtr hdc, [In] ref BITMAPINFO bitmapInfo, int iUsage, [Out] out IntPtr ppvBits, IntPtr hSection, int dwOffset);

        public static SafeHBITMAP CreateDIBSection(SafeDC? hdc, ref BITMAPINFO bitmapInfo, out IntPtr ppvBits, IntPtr hSection, int dwOffset)
        {
            const int DIB_RGB_COLORS = 0;
            SafeHBITMAP? hBitmap = null;
            if (hdc is null)
            {
                hBitmap = _CreateDIBSectionIntPtr(IntPtr.Zero, ref bitmapInfo, DIB_RGB_COLORS, out ppvBits, hSection, dwOffset);
            }
            else
            {
                hBitmap = _CreateDIBSection(hdc, ref bitmapInfo, DIB_RGB_COLORS, out ppvBits, hSection, dwOffset);
            }

            if (hBitmap.IsInvalid)
            {
                HRESULT.ThrowLastError();
            }

            return hBitmap;
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn", SetLastError = true)]
        private static extern IntPtr _CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse)
        {
            IntPtr ret = _CreateRoundRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect, nWidthEllipse, nHeightEllipse);
            if (ret == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            return ret;
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgn", SetLastError = true)]
        private static extern IntPtr _CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
        {
            IntPtr ret = _CreateRectRgn(nLeftRect, nTopRect, nRightRect, nBottomRect);
            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRectRgnIndirect", SetLastError = true)]
        private static extern IntPtr _CreateRectRgnIndirect([In] ref RECT lprc);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr CreateRectRgnIndirect(RECT lprc)
        {
            IntPtr ret = _CreateRectRgnIndirect(ref lprc);
            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "CreateWindowExW")]
        private static extern IntPtr _CreateWindowEx(
            WS_EX dwExStyle,
            [MarshalAs(UnmanagedType.LPWStr)] string lpClassName,
            [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName,
            WS dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr CreateWindowEx(
            WS_EX dwExStyle,
            string lpClassName,
            string lpWindowName,
            WS dwStyle,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hWndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam)
        {
            IntPtr ret = _CreateWindowEx(dwExStyle, lpClassName, lpWindowName, dwStyle, x, y, nWidth, nHeight, hWndParent, hMenu, hInstance, lpParam);
            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "DefWindowProcW")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr handle);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hwnd);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll", EntryPoint = "DwmGetColorizationColor", PreserveSig = true)]
        private static extern HRESULT _DwmGetColorizationColor(out uint pcrColorization, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

        [CLSCompliant(false)]
        public static bool DwmGetColorizationColor(out uint pcrColorization, out bool pfOpaqueBlend)
        {
            // Make this call safe to make on downlevel OSes...
            if (Utility.IsOSVistaOrNewer && IsThemeActive())
            {
                HRESULT hr = _DwmGetColorizationColor(out pcrColorization, out pfOpaqueBlend);
                if (hr.Succeeded)
                {
                    return true;
                }
            }

            // Default values.  If for some reason the native DWM API fails it's never enough of a reason
            // to bring down the app.  Empirically it still sometimes returns errors even when the theme service is on.
            // We'll still use the boolean return value to allow the caller to respond if they care.
            pcrColorization = 0xFF000000;
            pfOpaqueBlend = true;

            return false;
        }

        //#define DWM_SIT_DISPLAYFRAME    0x00000001  // Display a window frame around the provided bitmap

        [DllImport("dwmapi.dll", EntryPoint = "DwmGetCompositionTimingInfo")]
        private static extern HRESULT _DwmGetCompositionTimingInfo(IntPtr hwnd, ref DWM_TIMING_INFO pTimingInfo);

        [CLSCompliant(false)]
        public static DWM_TIMING_INFO? DwmGetCompositionTimingInfo(IntPtr hwnd)
        {
            if (!Utility.IsOSVistaOrNewer)
            {
                // API was new to Vista.
                return null;
            }

            var dti = new DWM_TIMING_INFO { cbSize = Marshal.SizeOf(typeof(DWM_TIMING_INFO)) };
            HRESULT hr = _DwmGetCompositionTimingInfo(hwnd, ref dti);
            if (hr == HRESULT.E_PENDING)
            {
                // The system isn't yet ready to respond.  Return null rather than throw.
                return null;
            }

            hr.ThrowIfFailed();

            return dti;
        }

        [DllImport("dwmapi.dll", EntryPoint = "DwmIsCompositionEnabled", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _DwmIsCompositionEnabled();

        public static bool DwmIsCompositionEnabled()
        {
            // Make this call safe to make on downlevel OSes...
            if (!Utility.IsOSVistaOrNewer)
            {
                return false;
            }

            return _DwmIsCompositionEnabled();
        }

        [DllImport("dwmapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmDefWindowProc(IntPtr hwnd, WM msg, IntPtr wParam, IntPtr lParam, out IntPtr plResult);

        [DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute")]
        private static extern void _DwmSetWindowAttribute(IntPtr hwnd, DWMWA dwAttribute, ref int pvAttribute, int cbAttribute);

        public static void DwmSetWindowAttributeFlip3DPolicy(IntPtr hwnd, DWMFLIP3D flip3dPolicy)
        {
            Assert.IsTrue(Utility.IsOSVistaOrNewer);
            var dwPolicy = (int)flip3dPolicy;
            _DwmSetWindowAttribute(hwnd, DWMWA.FLIP3D_POLICY, ref dwPolicy, sizeof(int));
        }

        public static void DwmSetWindowAttributeDisallowPeek(IntPtr hwnd, bool disallowPeek)
        {
            Assert.IsTrue(Utility.IsOSWindows7OrNewer);
            int dwDisallow = (int)(disallowPeek ? Win32Value.TRUE : Win32Value.FALSE);
            _DwmSetWindowAttribute(hwnd, DWMWA.DISALLOW_PEEK, ref dwDisallow, sizeof(int));
        }

        [DllImport("user32.dll", EntryPoint = "EnableMenuItem")]
        private static extern int _EnableMenuItem(IntPtr hMenu, SC uIDEnableItem, MF uEnable);

        [CLSCompliant(false)]
        public static MF EnableMenuItem(IntPtr hMenu, SC uIDEnableItem, MF uEnable)
        {
            // Returns the previous state of the menu item, or -1 if the menu item does not exist.
            int iRet = _EnableMenuItem(hMenu, uIDEnableItem, uEnable);
            return (MF)iRet;
        }

        [DllImport("user32.dll", EntryPoint = "RemoveMenu", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [CLSCompliant(false)]
        public static void RemoveMenu(IntPtr hMenu, SC uPosition, MF uFlags)
        {
            if (!_RemoveMenu(hMenu, (uint)uPosition, (uint)uFlags))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _DrawMenuBar(IntPtr hWnd);

        public static void DrawMenuBar(IntPtr hWnd)
        {
            if (!_DrawMenuBar(hWnd))
            {
                throw new Win32Exception();
            }
        }

        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindClose(IntPtr handle);

        // Not shimming this SetLastError=true function because callers want to evaluate the reason for failure.
        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        After:
                [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        */

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern SafeFindHandle FindFirstFileW(string lpFileName, [In, Out, MarshalAs(UnmanagedType.LPStruct)] WIN32_FIND_DATAW lpFindFileData);

        // Not shimming this SetLastError=true function because callers want to evaluate the reason for failure.
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FindNextFileW(SafeFindHandle hndFindFile, [In, Out, MarshalAs(UnmanagedType.LPStruct)] WIN32_FIND_DATAW lpFindFileData);

        [DllImport("user32.dll", EntryPoint = "GetClientRect", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetClientRect(IntPtr hwnd, [Out] out RECT lpRect);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static RECT GetClientRect(IntPtr hwnd)
        {
            RECT rc;
            if (!_GetClientRect(hwnd, out rc))
            {
                HRESULT.ThrowLastError();
            }

            return rc;
        }

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "GetCursorPos", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetCursorPos(out POINT lpPoint);

        [SecurityCritical]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static POINT GetCursorPos()
        {
            POINT pt;
            if (!_GetCursorPos(out pt))
            {
                HRESULT.ThrowLastError();
            }

            return pt;
        }

        [SecurityCritical]
        public static bool TryGetCursorPos(out POINT pt)
        {
            var returnValue = _GetCursorPos(out pt);
            // Sometimes Win32 will fail this call, such as if you are
            // not running in the interactive desktop. For example,
            // a secure screen saver may be running.
            if (!returnValue)
            {
                System.Diagnostics.Debug.WriteLine("GetCursorPos failed!");
                pt.X = 0;
                pt.Y = 0;
            }

            return returnValue;
        }

        [SecurityCritical]
        [DllImport("user32.dll", EntryPoint = "GetPhysicalCursorPos", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetPhysicalCursorPos(out POINT lpPoint);

        [SecurityCritical]
        public static POINT GetPhysicalCursorPos()
        {
            POINT pt;
            if (!_GetPhysicalCursorPos(out pt))
            {
                HRESULT.ThrowLastError();
            }

            return pt;
        }

        /// <summary>
        /// Try to get the relative mouse position to the given handle in client coordinates.
        /// </summary>
        /// <param name="hWnd">The handle for this method.</param>
        /// <param name="point">The relative mouse position to the given handle.</param>
        public static bool TryGetRelativeMousePosition(IntPtr hWnd, out System.Windows.Point point)
        {
            POINT pt = default(POINT);
            var returnValue = hWnd != IntPtr.Zero && TryGetPhysicalCursorPos(out pt);
            if (returnValue)
            {
                ScreenToClient(hWnd, ref pt);
                point = new System.Windows.Point(pt.X, pt.Y);
            }
            else
            {
                point = default(System.Windows.Point);
            }

            return returnValue;
        }

        [SecurityCritical]
        public static bool TryGetPhysicalCursorPos(out POINT pt)
        {
            var returnValue = _GetPhysicalCursorPos(out pt);
            // Sometimes Win32 will fail this call, such as if you are
            // not running in the interactive desktop. For example,
            // a secure screen saver may be running.
            if (!returnValue)
            {
                System.Diagnostics.Debug.WriteLine("GetPhysicalCursorPos failed!");
                pt.X = 0;
                pt.Y = 0;
            }

            return returnValue;
        }

        [DllImport("uxtheme.dll", EntryPoint = "GetCurrentThemeName", CharSet = CharSet.Unicode)]
        private static extern HRESULT _GetCurrentThemeName(
            StringBuilder pszThemeFileName,
            int dwMaxNameChars,
            StringBuilder pszColorBuff,
            int cchMaxColorChars,
            StringBuilder pszSizeBuff,
            int cchMaxSizeChars);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void GetCurrentThemeName(out string themeFileName, out string color, out string size)
        {
            // Not expecting strings longer than MAX_PATH.  We will return the error 
            var fileNameBuilder = new StringBuilder((int)Win32Value.MAX_PATH);
            var colorBuilder = new StringBuilder((int)Win32Value.MAX_PATH);
            var sizeBuilder = new StringBuilder((int)Win32Value.MAX_PATH);

            // This will throw if the theme service is not active (e.g. not UxTheme!IsThemeActive).
            _GetCurrentThemeName(fileNameBuilder, fileNameBuilder.Capacity,
                                 colorBuilder, colorBuilder.Capacity,
                                 sizeBuilder, sizeBuilder.Capacity)
                .ThrowIfFailed();

            themeFileName = fileNameBuilder.ToString();
            color = colorBuilder.ToString();
            size = sizeBuilder.ToString();
        }

        [DllImport("uxtheme.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsThemeActive();

        [Obsolete("Use SafeDC.GetDC instead.", true)]
        public static void GetDC()
        {
        }

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(SafeDC hdc, DeviceCap nIndex);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleFileName", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int _GetModuleFileName(IntPtr hModule, StringBuilder lpFilename, int nSize);

        public static string GetModuleFileName(IntPtr hModule)
        {
            var buffer = new StringBuilder((int)Win32Value.MAX_PATH);
            while (true)
            {
                int size = _GetModuleFileName(hModule, buffer, buffer.Capacity);
                if (size == 0)
                {
                    HRESULT.ThrowLastError();
                }

                // GetModuleFileName returns nSize when it's truncated but does NOT set the last error.
                // MSDN documentation says this has changed in Windows 2000+.
                if (size == buffer.Capacity)
                {
                    // Enlarge the buffer and try again.
                    buffer.EnsureCapacity(buffer.Capacity * 2);
                    continue;
                }

                return buffer.ToString();
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr _GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string? lpModuleName);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr GetModuleHandle(string? lpModuleName)
        {
            IntPtr retPtr = _GetModuleHandle(lpModuleName);
            if (retPtr == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return retPtr;
        }

        [DllImport("user32.dll", EntryPoint = "GetMonitorInfo", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetMonitorInfo([In] IntPtr hMonitor, ref MONITORINFO lpmi);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static MONITORINFO GetMonitorInfo([In] IntPtr hMonitor)
        {
            var mi = new MONITORINFO
            {
                cbSize = Marshal.SizeOf(typeof(MONITORINFO))
            };
            if (!_GetMonitorInfo(hMonitor, ref mi))
            {
                HRESULT.ThrowLastError();
            }

            return mi;
        }

        [DllImport("user32.dll", EntryPoint = "GetMonitorInfoW", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetMonitorInfoW([In] IntPtr hMonitor, ref MONITORINFO lpmi);

        public static MONITORINFO GetMonitorInfoW([In] IntPtr hMonitor)
        {
            var mi = new MONITORINFO
            {
                cbSize = Marshal.SizeOf(typeof(MONITORINFO))
            };
            if (!_GetMonitorInfoW(hMonitor, ref mi))
            {
                HRESULT.ThrowLastError();
            }

            return mi;
        }

        public static IntPtr GetTaskBarHandleForMonitor(IntPtr monitor)
        {
            // maybe we can use ReBarWindow32 isntead Shell_TrayWnd
            var hwnd = NativeMethods.FindWindow("Shell_TrayWnd", null);
            var monitorWithTaskbarOnIt = NativeMethods.MonitorFromWindow(hwnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            if (!Equals(monitor, monitorWithTaskbarOnIt))
            {
                hwnd = NativeMethods.FindWindow("Shell_SecondaryTrayWnd", null);
                monitorWithTaskbarOnIt = NativeMethods.MonitorFromWindow(hwnd, MonitorOptions.MONITOR_DEFAULTTONEAREST);

                if (!Equals(monitor, monitorWithTaskbarOnIt))
                {
                    return IntPtr.Zero;
                }
            }

            return hwnd;
        }

        [DllImport("gdi32.dll", EntryPoint = "GetStockObject", SetLastError = true)]
        private static extern IntPtr _GetStockObject(StockObject fnObject);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr GetStockObject(StockObject fnObject)
        {
            IntPtr retPtr = _GetStockObject(fnObject);
            if (retPtr == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return retPtr;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SM nIndex);

        [DllImport("kernel32.dll")]
        [CLSCompliant(false)]
        public static extern uint GetCurrentThreadId();

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        [CLSCompliant(false)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getwindow
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hwnd, GW nCmd);

        [DllImport("user32.dll", CharSet = CharSet.None, SetLastError = true, EntryPoint = "GetWindowInfo")]
        private static extern bool _GetWindowInfo(IntPtr hWnd, ref WINDOWINFO pwi);

        public static WINDOWINFO GetWindowInfo(IntPtr hWnd)
        {
            WINDOWINFO info = new WINDOWINFO()
            {
                cbSize = Marshal.SizeOf(typeof(WINDOWINFO))
            };
            if (!_GetWindowInfo(hWnd, ref info))
            {
                HRESULT.ThrowLastError();
            }

            return info;
        }

        [CLSCompliant(false)]
        public static WS GetWindowStyle(IntPtr hWnd)
        {
            return (WS)GetWindowLongPtr(hWnd, GWL.STYLE);
        }

        [CLSCompliant(false)]
        public static WS_EX GetWindowStyleEx(IntPtr hWnd)
        {
            return (WS_EX)GetWindowLongPtr(hWnd, GWL.EXSTYLE);
        }

        [CLSCompliant(false)]
        public static WS SetWindowStyle(IntPtr hWnd, WS dwNewLong)
        {
            return (WS)SetWindowLongPtr(hWnd, GWL.STYLE, (IntPtr)(int)dwNewLong);
        }

        [CLSCompliant(false)]
        public static WS_EX SetWindowStyleEx(IntPtr hWnd, WS_EX dwNewLong)
        {
            return (WS_EX)SetWindowLongPtr(hWnd, GWL.EXSTYLE, (IntPtr)(int)dwNewLong);
        }

        // This is aliased as a macro in 32bit Windows.
        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                [MethodImpl(MethodImplOptions.NoInlining)]
        After:
                [MethodImpl(MethodImplOptions.NoInlining)]
        */

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr GetWindowLongPtr(IntPtr hwnd, GWL nIndex)
        {
            var ret = IntPtr.Size == 8
                ? GetWindowLongPtr64(hwnd, nIndex)
                : GetWindowLongPtr32(hwnd, nIndex);

            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        public static IntPtr GetClassLong(IntPtr hWnd, GCLP nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return new IntPtr(GetClassLong32(hWnd, nIndex));
            }

            return GetClassLong64(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern uint GetClassLong32(IntPtr hWnd, GCLP nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLong64(IntPtr hWnd, GCLP nIndex);

        public static IntPtr SetClassLong(IntPtr hWnd, GCLP nIndex, IntPtr value)
        {
            if (IntPtr.Size == 4)
            {
                return new IntPtr(SetClassLong32(hWnd, nIndex, value));
            }

            return SetClassLong64(hWnd, nIndex, value);
        }

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        private static extern uint SetClassLong32(IntPtr hWnd, GCLP nIndex, IntPtr value);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLong64(IntPtr hWnd, GCLP nIndex, IntPtr value);

        [DllImport("user32.dll", EntryPoint = "SetProp", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SetProp(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string lpString, IntPtr hData);

        public static void SetProp(IntPtr hwnd, string lpString, IntPtr hData)
        {
            if (!_SetProp(hwnd, lpString, hData))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        /// <summary>
        /// Sets attributes to control how visual styles are applied to a specified window.
        /// </summary>
        /// <param name="hwnd">
        /// Handle to a window to apply changes to.
        /// </param>
        /// <param name="eAttribute">
        /// Value of type WINDOWTHEMEATTRIBUTETYPE that specifies the type of attribute to set.
        /// The value of this parameter determines the type of data that should be passed in the pvAttribute parameter.
        /// Can be the following value:
        /// <list>WTA_NONCLIENT (Specifies non-client related attributes).</list>
        /// pvAttribute must be a pointer of type WTA_OPTIONS.
        /// </param>
        /// <param name="pvAttribute">
        /// A pointer that specifies attributes to set. Type is determined by the value of the eAttribute value.
        /// </param>
        /// <param name="cbAttribute">
        /// Specifies the size, in bytes, of the data pointed to by pvAttribute.
        /// </param>
        [DllImport("uxtheme.dll", PreserveSig = false)]
        [CLSCompliant(false)]
        public static extern void SetWindowThemeAttribute([In] IntPtr hwnd, [In] WINDOWTHEMEATTRIBUTETYPE eAttribute, [In] ref WTA_OPTIONS pvAttribute, [In] uint cbAttribute);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, GWL nIndex);

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to the WINDOWPLACEMENT structure that receives the show state and position information.</param>
        /// <remarks>
        /// Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT).
        /// GetWindowPlacement fails if lpwndpl-> length is not set correctly.
        /// </remarks>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hwnd, [In, Out] WINDOWPLACEMENT lpwndpl);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static WINDOWPLACEMENT GetWindowPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT wndpl = new WINDOWPLACEMENT();
            if (GetWindowPlacement(hwnd, wndpl))
            {
                return wndpl;
            }

            throw new Win32Exception();
        }

        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpwndpl">A pointer to a WINDOWPLACEMENT structure that specifies the new show state and window positions.</param>
        /// <remarks>
        /// Before calling SetWindowPlacement, set the length member of the WINDOWPLACEMENT structure to sizeof(WINDOWPLACEMENT).
        /// SetWindowPlacement fails if the length member is not set correctly.
        /// </remarks>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", EntryPoint = "GetWindowRect", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static RECT GetWindowRect(IntPtr hwnd)
        {
            RECT rc;
            if (!_GetWindowRect(hwnd, out rc))
            {
                HRESULT.ThrowLastError();
            }

            return rc;
        }

        /// <summary>
        /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar).
        /// If the specified window is a control, the function retrieves the length of the text within the control. However,
        /// GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control.</param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the text. Under certain
        /// conditions, this value may actually be greater than the length of the text. For more information, see the following
        /// Remarks section.
        /// <para>If the window has no text, the return value is zero. To get extended error information, call GetLastError.</para>
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer. If the specified window is
        /// a control, the text of the control is copied. However, GetWindowText cannot retrieve the text of a control in another
        /// application.
        /// </summary>
        /// <param name="hWnd">A handle to the window or control containing the text.</param>
        /// <param name="strText">
        /// The buffer that will receive the text. If the string is as long or longer than the buffer, the
        /// string is truncated and terminated with a null character.
        /// </param>
        /// <param name="maxCount">
        /// The maximum number of characters to copy to the buffer, including the null character. If the
        /// text exceeds this limit, it is truncated.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the copied string, not including
        /// the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the window or
        /// control handle is invalid, the return value is zero. To get extended error information, call GetLastError.
        /// <para>This function cannot retrieve the text of an edit control in another application.</para>
        /// </returns>
        [PublicAPI]
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("gdiplus.dll")]
        public static extern Status GdipCreateBitmapFromStream(IStream stream, out IntPtr bitmap);

        [DllImport("gdiplus.dll")]
        public static extern Status GdipCreateHBITMAPFromBitmap(IntPtr bitmap, out IntPtr hbmReturn, int background);

        [DllImport("gdiplus.dll")]
        public static extern Status GdipCreateHICONFromBitmap(IntPtr bitmap, out IntPtr hbmReturn);

        [DllImport("gdiplus.dll")]
        public static extern Status GdipDisposeImage(IntPtr image);

        [DllImport("gdiplus.dll")]
        public static extern Status GdipImageForceValidation(IntPtr image);

        [DllImport("gdiplus.dll")]
        public static extern Status GdiplusStartup(out IntPtr token, StartupInput input, out StartupOutput output);

        [DllImport("gdiplus.dll")]
        public static extern Status GdiplusShutdown(IntPtr token);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsZoomed(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hwnd);

        [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
        private static extern IntPtr _LocalFree(IntPtr hMem);

        [DllImport("user32.dll")]
        [CLSCompliant(false)]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, MonitorOptions dwFlags);

        [DllImport("user32.dll")]
        [CLSCompliant(false)]
        public static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        /// <summary>
        /// The MonitorFromRect function retrieves a handle to the display monitor that 
        /// has the largest area of intersection with a specified rectangle.
        /// </summary>
        /// <param name="lprc">Pointer to a RECT structure that specifies the rectangle of interest in 
        /// virtual-screen coordinates</param>
        /// <param name="dwFlags">Determines the function's return value if the rectangle does not intersect 
        /// any display monitor</param>
        /// <returns>
        /// If the rectangle intersects one or more display monitor rectangles, the return value 
        /// is an HMONITOR handle to the display monitor that has the largest area of intersection with the rectangle.
        /// If the rectangle does not intersect a display monitor, the return value depends on the value of dwFlags.
        /// </returns>
        [DllImport("user32.dll")]
        [CLSCompliant(false)]
        public static extern IntPtr MonitorFromRect([In] ref RECT lprc, MonitorOptions dwFlags);

        /// <summary>
        /// Loads an icon, cursor, animated cursor, or bitmap.
        /// </summary>
        /// <param name="hinst">Handle to the module of either a DLL or executable (.exe) that contains the image to be loaded</param>
        /// <param name="lpszName">Specifies the image to load</param>
        /// <param name="uType">Specifies the type of image to be loaded. </param>
        /// <param name="cxDesired">Specifies the width, in pixels, of the icon or cursor</param>
        /// <param name="cyDesired">Specifies the height, in pixels, of the icon or cursor</param>
        /// <param name="fuLoad">This parameter can be one or more of the following values.</param>
        /// <returns>If the function succeeds, the return value is the requested value.If the function fails, the return value is zero. To get extended error information, call GetLastError. </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [CLSCompliant(false)]
        public static extern IntPtr LoadImage(IntPtr hinst, IntPtr lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [CLSCompliant(false)]
        public static extern int ToUnicode(uint virtualKey, uint scanCode, byte[] keyStates, [MarshalAs(UnmanagedType.LPArray)][Out] char[] chars, int charMaxCount, uint flags);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        [CLSCompliant(false)]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        // ReSharper disable InconsistentNaming
        [CLSCompliant(false)]
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3,
        }

        [DllImport("user32.dll", EntryPoint = "PostMessage", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _PostMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void PostMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam)
        {
            if (!_PostMessage(hWnd, Msg, wParam, lParam))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassExW")]
        private static extern short _RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        // Note that this will throw HRESULT_FROM_WIN32(ERROR_CLASS_ALREADY_EXISTS) on duplicate registration.
        // If needed, consider adding a Try* version of this function that returns the error code since that
        // may be ignorable.
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static short RegisterClassEx(ref WNDCLASSEX lpwcx)
        {
            short ret = _RegisterClassEx(ref lpwcx);
            if (ret == 0)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("user32.dll", EntryPoint = "RegisterWindowMessage", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern uint _RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string lpString);

        public static WM RegisterWindowMessage(string lpString)
        {
            uint iRet = _RegisterWindowMessage(lpString);
            if (iRet == 0)
            {
                HRESULT.ThrowLastError();
            }

            return (WM)iRet;
        }

        [DllImport("user32.dll", EntryPoint = "SetActiveWindow", SetLastError = true)]
        private static extern IntPtr _SetActiveWindow(IntPtr hWnd);

        public static IntPtr SetActiveWindow(IntPtr hwnd)
        {
            Verify.IsNotDefault(hwnd, "hwnd");
            IntPtr ret = _SetActiveWindow(hwnd);
            if (ret == IntPtr.Zero
                && (HRESULT)Win32Error.GetLastError() != HRESULT.S_OK)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        // This is aliased as a macro in 32bit Windows.
        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                public static IntPtr SetClassLongPtr(IntPtr hwnd, GCLP nIndex, IntPtr dwNewLong)
        After:
                public static IntPtr SetClassLongPtr(IntPtr hwnd, GCLP nIndex, IntPtr dwNewLong)
        */

        public static IntPtr SetClassLongPtr(IntPtr hwnd, GCLP nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
            {
                return SetClassLongPtr64(hwnd, nIndex, dwNewLong);
            }

            return new IntPtr(SetClassLongPtr32(hwnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        [CLSCompliant(false)]
        public static extern bool FlashWindowEx(ref FLASHWINFO flashInfo);

        [StructLayout(LayoutKind.Sequential)]
        [CLSCompliant(false)]
        public struct FLASHWINFO
        {
            public uint cbSize;
            public IntPtr hwnd;
            public FlashWindowFlag dwFlags;
            public uint uCount;
            public uint dwTimeout;
        }

        [CLSCompliant(false)]
        public enum FlashWindowFlag : uint
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state. 
            /// </summary>    
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption 
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button. 
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags. 
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground. 
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        [DllImport("user32.dll", EntryPoint = "SetClassLong", SetLastError = true)]
        private static extern int SetClassLongPtr32(IntPtr hWnd, GCLP nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr", SetLastError = true)]
        private static extern IntPtr SetClassLongPtr64(IntPtr hWnd, GCLP nIndex, IntPtr dwNewLong);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern ErrorModes SetErrorMode(ErrorModes newMode);

        [DllImport("kernel32.dll", SetLastError = true, EntryPoint = "SetProcessWorkingSetSize")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SetProcessWorkingSetSize(IntPtr hProcess, IntPtr dwMinimiumWorkingSetSize, IntPtr dwMaximumWorkingSetSize);

        public static void SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize)
        {
            if (!_SetProcessWorkingSetSize(hProcess, new IntPtr(dwMinimumWorkingSetSize), new IntPtr(dwMaximumWorkingSetSize)))
            {
                throw new Win32Exception();
            }
        }

        // This is aliased as a macro in 32bit Windows.
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IntPtr SetWindowLongPtr(IntPtr hwnd, GWL nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
            {
                return SetWindowLongPtr64(hwnd, nIndex, dwNewLong);
            }

            return new IntPtr(SetWindowLongPtr32(hwnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, GWL nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowRgn", SetLastError = true)]
        private static extern int _SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw)
        {
            int err = _SetWindowRgn(hWnd, hRgn, bRedraw);
            if (err == 0)
            {
                throw new Win32Exception();
            }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SWP uFlags);

        [CLSCompliant(false)]
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SWP uFlags)
        {
            if (!_SetWindowPos(hWnd, hWndInsertAfter, X, Y, cx, cy, uFlags))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("shell32.dll", SetLastError = false)]
        public static extern Win32Error SHFileOperation(ref SHFILEOPSTRUCT lpFileOp);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SystemParametersInfo_String(SPI uiAction, int uiParam, [MarshalAs(UnmanagedType.LPWStr)] string pvParam, SPIF fWinIni);

        /// <summary>Overload of SystemParametersInfo for getting and setting NONCLIENTMETRICS.</summary>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SystemParametersInfo_NONCLIENTMETRICS(SPI uiAction, int uiParam, [In, Out] ref NONCLIENTMETRICS pvParam, SPIF fWinIni);

        /// <summary>Overload of SystemParametersInfo for getting and setting HIGHCONTRAST.</summary>
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _SystemParametersInfo_HIGHCONTRAST(SPI uiAction, int uiParam, [In, Out] ref HIGHCONTRAST pvParam, SPIF fWinIni);

        public static void SystemParametersInfo(SPI uiAction, int uiParam, string pvParam, SPIF fWinIni)
        {
            if (!_SystemParametersInfo_String(uiAction, uiParam, pvParam, fWinIni))
            {
                HRESULT.ThrowLastError();
            }
        }

        public static NONCLIENTMETRICS SystemParameterInfo_GetNONCLIENTMETRICS()
        {
            var metrics = Utility.IsOSVistaOrNewer
                ? NONCLIENTMETRICS.VistaMetricsStruct
                : NONCLIENTMETRICS.XPMetricsStruct;

            if (!_SystemParametersInfo_NONCLIENTMETRICS(SPI.GETNONCLIENTMETRICS, metrics.cbSize, ref metrics, SPIF.None))
            {
                HRESULT.ThrowLastError();
            }

            return metrics;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static HIGHCONTRAST SystemParameterInfo_GetHIGHCONTRAST()
        {
            var hc = new HIGHCONTRAST { cbSize = Marshal.SizeOf(typeof(HIGHCONTRAST)) };

            if (!_SystemParametersInfo_HIGHCONTRAST(SPI.GETHIGHCONTRAST, hc.cbSize, ref hc, SPIF.None))
            {
                HRESULT.ThrowLastError();
            }

            return hc;
        }

        // This function is strange in that it returns a BOOL if TPM_RETURNCMD isn't specified, but otherwise the command Id.

        /* Unmerged change from project 'ControlzEx (net5.0-windows)'
        Before:
                // Currently it's only used with TPM_RETURNCMD, so making the signature match that.
                [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        After:
                // Currently it's only used with TPM_RETURNCMD, so making the signature match that.
        */
        // Currently it's only used with TPM_RETURNCMD, so making the signature match that.
        [DllImport("user32.dll")]
        [CLSCompliant(false)]
        public static extern uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
        private static extern IntPtr _SelectObject(SafeDC hdc, IntPtr hgdiobj);

        public static IntPtr SelectObject(SafeDC hdc, IntPtr hgdiobj)
        {
            IntPtr ret = _SelectObject(hdc, hgdiobj);
            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("gdi32.dll", EntryPoint = "SelectObject", SetLastError = true)]
        private static extern IntPtr _SelectObjectSafeHBITMAP(SafeDC hdc, SafeHBITMAP hgdiobj);

        public static IntPtr SelectObject(SafeDC hdc, SafeHBITMAP hgdiobj)
        {
            IntPtr ret = _SelectObjectSafeHBITMAP(hdc, hgdiobj);
            if (ret == IntPtr.Zero)
            {
                HRESULT.ThrowLastError();
            }

            return ret;
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

        // Depending on the message, callers may want to call GetLastError based on the return value.
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hwnd, SW nCmdShow);

        [DllImport("user32.dll", EntryPoint = "UnregisterClass", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _UnregisterClassAtom(IntPtr lpClassName, IntPtr hInstance);

        [DllImport("user32.dll", EntryPoint = "UnregisterClass", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _UnregisterClassName(string lpClassName, IntPtr hInstance);

        public static void UnregisterClass(short atom, IntPtr hinstance)
        {
            if (!_UnregisterClassAtom(new IntPtr(atom), hinstance))
            {
                HRESULT.ThrowLastError();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void UnregisterClass(string lpClassName, IntPtr hInstance)
        {
            if (!_UnregisterClassName(lpClassName, hInstance))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "UpdateLayeredWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _UpdateLayeredWindow(
            IntPtr hwnd,
            SafeDC hdcDst,
            [In] ref POINT pptDst,
            [In] ref SIZE psize,
            SafeDC hdcSrc,
            [In] ref POINT pptSrc,
            int crKey,
            ref BLENDFUNCTION pblend,
            ULW dwFlags);

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "UpdateLayeredWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool _UpdateLayeredWindowIntPtr(
            IntPtr hwnd,
            IntPtr hdcDst,
            IntPtr pptDst,
            IntPtr psize,
            IntPtr hdcSrc,
            IntPtr pptSrc,
            int crKey,
            ref BLENDFUNCTION pblend,
            ULW dwFlags);

        public static void UpdateLayeredWindow(
            IntPtr hwnd,
            SafeDC hdcDst,
            ref POINT pptDst,
            ref SIZE psize,
            SafeDC hdcSrc,
            ref POINT pptSrc,
            int crKey,
            ref BLENDFUNCTION pblend,
            ULW dwFlags)
        {
            if (!_UpdateLayeredWindow(hwnd, hdcDst, ref pptDst, ref psize, hdcSrc, ref pptSrc, crKey, ref pblend, dwFlags))
            {
                HRESULT.ThrowLastError();
            }
        }

        public static void UpdateLayeredWindow(
            IntPtr hwnd,
            int crKey,
            ref BLENDFUNCTION pblend,
            ULW dwFlags)
        {
            if (!_UpdateLayeredWindowIntPtr(hwnd, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, crKey, ref pblend, dwFlags))
            {
                HRESULT.ThrowLastError();
            }
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string? lpWindowName);

        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall)]
        [CLSCompliant(false)]
        public static extern uint SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        #region Win7 declarations

        //#define DWM_SIT_DISPLAYFRAME    0x00000001  // Display a window frame around the provided bitmap

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmInvalidateIconicBitmaps(IntPtr hwnd);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmSetIconicThumbnail(IntPtr hwnd, IntPtr hbmp, DWM_SIT dwSITFlags);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmSetIconicLivePreviewBitmap(IntPtr hwnd, IntPtr hbmp, RefPOINT pptClient, DWM_SIT dwSITFlags);

        [DllImport("shell32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        [CLSCompliant(false)]
        public static extern bool Shell_NotifyIcon(NIM dwMessage, [In] NOTIFYICONDATA lpdata);

        #endregion
    }
}