using Amazon;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using TrackManagement;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace PollReflexCentralForNewTracks
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(Amazon.Lambda.SQSEvents.SQSEvent sqsEvent, ILambdaContext context)
        {
            try
            {                
                PollTracks.Poll(sqsEvent, context, false);
            }
            catch (Exception e)
            {
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
            }
        }
        
    }
}
