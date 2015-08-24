using System;


public class TelemachusNavutilitiesPlugin
{
    public string[] Commands {
        get { 
            return new[] { "navutil.*" };
        }
    }

    public Func<Vessel, string[], object> GetAPIHandler(string API)
    {
        if (SimpleValues.ContainsKey(API.ToLowerInvariant()) {
            return (v, a) => SimpleValues[API.ToLowerInvariant()](NavUtilLib.GlobalVariables.FlightData);
        }
        return null;
    }

    private static Dictionary<string, Func<FlightData, float>> SimpleValues = new Dictionary<string, Func<FlightData, float>>() {
        {"navutil.glideslope",     fd => fd.selectedGlideSlope},
        {"navutil.bearing",        fd => fd.bearing},
        {"navutil.dme",            fd => fd.dme},
        {"navutil.elevationangle", fd => fd.elevationAngle},
        {"navutil.locdeviation",   fd => fd.locDeviation},
        {"navutil.gsdeviation",    fd => fd.gsDeviation},
        {"navutil.runwayheading",  fd => fd.runwayHeading},
    };


}