using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text;

namespace HybridAi.TestTask.Data.Extensions
{
    public static class IPAddressExtensions
    {
        /// <summary>
        /// Address: X.X.X.X or XXXX::XXXX::
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string ToStringByteArray( this string address )
        {
            try {
                var ip = IPAddress.Parse( address );
                var bytes = ip.GetAddressBytes();
                return BitConverter.ToString( bytes );
            }
            catch {
                return "";
            }
        }

        public static byte[] ToBytes( this string ipStrBytes )
        {
            var values = ipStrBytes.Split( '-' );
            var bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++) {
                bytes[i] = Convert.ToByte( values[i], 16 );
            }

            return bytes;
        }

        public static IPAddress ToIp( this string ipStringBytes )
        {
            var values = ipStringBytes.Split( '-' );
            var splitter = values.Length == 4 ? "." : ":" ;
            var sb = new StringBuilder();

            if ( values.Length == 4 ) {
                sb.Append( Convert.ToByte( values[0], 16 ) );
                for (int i = 1; i < values.Length; i++) {
                    sb.Append( splitter ).Append( Convert.ToByte( values[i], 16 ) );
                }
            }
            else if ( values.Length == 16 ) {
                sb.Append( values[0] ).Append( values[1] );
                for (int i = 2; i < values.Length; i += 2) {
                    sb.Append( splitter ).Append( values[i] ).Append( values[ i + 1 ] );
                }
            }
            else {
                return null;
            }

            return IPAddress.Parse( sb.ToString() );
        }



        public static BigInteger ToBigInteger( this string network )
        {
            return BigInteger.Parse( "0" + network.Replace( "-", "" ), NumberStyles.AllowHexSpecifier );
        }
    }
}
