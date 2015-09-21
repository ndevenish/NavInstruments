using NavUtilLib;
using NavUtilLib.GlobalVariables;
using System;
using System.Collections.Generic;

public class TelemachusNavutilitiesPlugin
{
    public string[] Commands {
        get { 
            return new[] { "navutil.*" };
        }
    }

    public Func<Vessel, string[], object> GetAPIHandler(string API)
    {
        if (SimpleValues.ContainsKey(API.ToLowerInvariant()))
        {
            return (v, a) =>
            {
                if (FlightData.GetLastNavUpdateUT() + 0.05 < Planetarium.GetUniversalTime()) FlightData.updateNavigationData();
                return SimpleValues[API.ToLowerInvariant()]();
            };
        }
        
        return null;
    }

    private static Dictionary<string, object> GetRunwayInformation(Runway runway)
    {
        if (runway == null) return null;
        var info = new Dictionary<string, object>();
        info["markers"] = new[] { runway.outerMarkerDist, runway.middleMarkerDist, runway.innerMarkerDist };
        info["identity"] = runway.ident;
        info["altitude"] = runway.altMSL;
        info["heading"] = runway.hdg;
        return info;
    }

    private static Dictionary<string, Func<object>> SimpleValues = new Dictionary<string, Func<object>>() {
        {"navutil.glideslope",     () => FlightData.selectedGlideSlope},
        {"navutil.bearing",        () => FlightData.bearing},
        {"navutil.dme",            () => FlightData.dme},
        {"navutil.elevationangle", () => FlightData.elevationAngle},
        {"navutil.locdeviation",   () => FlightData.locDeviation},
        {"navutil.gsdeviation",    () => FlightData.gsDeviation},
        {"navutil.runwayheading",() => FlightData.runwayHeading},
        {"navutil.runway",         () => GetRunwayInformation(FlightData.selectedRwy) }, 

    };


}
