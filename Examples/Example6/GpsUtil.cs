using System;
using System.Collections.Generic;
using System.Text;

/*
 * 
 * DISCLAIMER OF WARRANTY: THIS SOFTWARE IS PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING,
 * WITHOUT LIMITATION, WARRANTIES THAT THE SOFTWARE IS FREE OF DEFECTS, MERCHANTABLE, FIT FOR A PARTICULAR PURPOSE OR NON-INFRINGING.
 * THE ENTIRE RISK AS TO THE QUALITY AND PERFORMANCE OF THE SOFTWARE IS WITH YOU. SHOULD ANY COVERED CODE PROVE DEFECTIVE IN ANY RESPECT,
 * YOU (NOT CORPORATE EASY GIS .NET) ASSUME THE COST OF ANY NECESSARY SERVICING, REPAIR OR CORRECTION.  
 * 
 * LIABILITY: IN NO EVENT SHALL CORPORATE EASY GIS .NET BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, 
 * DAMAGES FOR LOSS OF BUSINESS PROFITS, BUSINESS INTERRUPTION, LOSS OF INFORMATION OR ANY OTHER PECUNIARY LOSS)
 * ARISING OUT OF THE USE OF INABILITY TO USE THIS SOFTWARE, EVEN IF CORPORATE EASY GIS .NET HAS BEEN ADVISED OF THE POSSIBILITY
 * OF SUCH DAMAGES.
 * 
 * Copyright: Easy GIS .NET 2009
 *
 */
namespace Example6
{
    
    /// <summary>
    /// 
    /// </summary>
    public class GpsPacket
    {
        //		GGA - Global Positioning System fix Data
        //		GGA,123519,4807.038,N,01131.324,E,1,08,0.9,545.4,M,46.9,M, , *42
        //		
        //		123519       fix taken at 12:35:19 UTC
        //      4807.038,N   Latitude 48 deg 07.038' N
        //		01131.324,E  Longitude 11 deg 31.324' E
        //		1            fix quality: 0 = invalid
        //								  1 = GPS fix
        //								  2 = DGPS fix
        //		08           Number of satellites being tracked
        //		0.9          Horizontal dilution of position
        //		545.4,M      Altitude, Metres, above mean sea level
        //		46.9,M       Height of geoid (mean sea level) above WGS84
        //		             ellipsoid
        //	    (empty)      time in seconds since last DGPS update
        //      (empty)      DGPS station ID number
        //		*42          Checksum
        //---------------------------------------------------------------------------------
        //Sample GPS output from GPS-48 USB Device
        //---------------------------------------------------------------------------------
        //$GPGGA,012711,3749.2776,S,14502.4100,E,1,07,02.04,000039.3,M,-001.8,M,,*7D
        //$GPZDA,012712,06,04,2004,+00,00*60
        //$GPGLL,3749.2776,S,14502.4100,E,012711,A,A*54
        //$GPGSA,A,3,01,08,13,16,20,27,31,,,,,,02.04,01.08,01.73*31
        //$GPGSV,3,1,12,01,51,080,43,02,39,127,31,03,19,087,29,04,05,284,29*77
        //$GPGSV,3,2,12,08,25,279,48,13,72,184,46,16,21,136,44,19,11,058,29*72
        //$GPGSV,3,3,12,20,20,007,45,24,06,250,31,27,54,256,48,31,14,049,38*71
        //$GPVTG,277.7,T,266.0,M,000.1,N,0000.2,K,A*17
        //---------------------------------------------------------------------------------

        private bool valid;
        private DateTime time;
        private bool fix;        
        private double lat, lon;

        public GpsPacket()
        {
            this.time = DateTime.Now;            
        }

        public GpsPacket(string packet)
        {
            //Example packet: $GPGGA,012711,3749.2776,S,14502.4100,E,1,07,02.04,000039.3,M,-001.8,M,,*7D
            this.time = DateTime.Now;

            string[] PacketTokens = packet.Split(new char[] { ',' });
            try
            {
                lat = GetLatitude(PacketTokens[2], PacketTokens[3]);
                lon = GetLongitude(PacketTokens[4], PacketTokens[5]);

                //Check the validity string - if data is correct, then use checksum to ensure
                //absolute correctness, otherwise mark the data as invalid
                string validChecksum = GetChecksum(packet);
                string packetChecksum = packet.Substring(packet.Length - 2, 2);

                this.valid = (validChecksum.CompareTo(packetChecksum) == 0);

                //Check the fix - if fix is greater than zero then the fix quality is good, else there is no fix
                fix = (int.Parse(PacketTokens[6]) > 0);
            }
            catch
            {
                this.valid = false;
            }
        }

        #region public properties

        public double Latitude
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
            }
        }

        public double Longitude
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
            }
        }

        public DateTime PacketTime
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        public bool Fix
        {
            get
            {
                return fix;
            }
            set
            {
                fix = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return valid;
            }
            set
            {
                valid = value;
            }
        }

        #endregion

        #region static utility methods

        static double GetLatitude(string latitudeToken, string direction)
        {
            //Latitude token: DDMM.mmm,N
            //Decimal Degrees = DD + (MM.mmm / 60)
            string Degrees = latitudeToken.Substring(0, 2);
            string Minutes = latitudeToken.Substring(2);
            double Latitude = double.Parse(Degrees) + (double.Parse(Minutes) / 60.0);
            //direction: N or S
            //If direction is South: * -1
            if (!direction.Equals("N", StringComparison.OrdinalIgnoreCase))  Latitude = -Latitude;
            return Latitude;
        }

        static double GetLongitude(string longitudeToken, string direction)
        {
            //Longtitude token: DDDMM.mmm,E
            //Decimal Degrees = DDD + (MM.mmm / 60)
            string Degrees = longitudeToken.Substring(0, 3);
            string Minutes = longitudeToken.Substring(3);
            double Longtitude = double.Parse(Degrees) + (double.Parse(Minutes) / 60.0);
            //direction: E or W
            //If direction is West: * -1
            if (!direction.Equals("E", StringComparison.OrdinalIgnoreCase)) Longtitude = -Longtitude;
            return Longtitude;
        }

        static string GetChecksum(string packet)
        {
            //Ignore the '$' at the start of the packet, and the checksum token eg.'*54'
            string truncatedPacket = packet.Substring(1, packet.Length - 4);

            char[] packetCharacters = truncatedPacket.ToCharArray();
            int lastChar;

            lastChar = Convert.ToInt32(packetCharacters[0]);

            //Use a loop to Xor through the string
            for (int i = 1; i < packetCharacters.Length; i++)
            {
                lastChar = lastChar ^ Convert.ToInt32(packetCharacters[i]);
            }

            return String.Format("{0:x2}", lastChar).ToUpper();
        }

        #endregion

    }
}
