// COPYRIGHT 1995-2021 ESRI
// TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
// Unpublished material - all rights reserved under the
// Copyright Laws of the United States and applicable international
// laws, treaties, and conventions.
//
// For additional information, contact:
// Environmental Systems Research Institute, Inc.
// Attn: Contracts and Legal Services Department
// 380 New York Street
// Redlands, California, 92373
// USA
//
// email: contracts@esri.com

using System.Runtime.InteropServices;
using System;

namespace Esri.GameEngine.Security
{
    public delegate void ArcGISAuthenticationChallengeIssuedEvent(ArcGISAuthenticationChallenge authenticationChallenge);
    
    internal delegate void ArcGISAuthenticationChallengeIssuedEventInternal(IntPtr userData, IntPtr authenticationChallenge);
    
    internal class ArcGISAuthenticationChallengeIssuedEventHandler : EventHandler<ArcGISAuthenticationChallengeIssuedEvent>
    {
        [MonoPInvokeCallback(typeof(ArcGISAuthenticationChallengeIssuedEventInternal))]
        internal static void HandlerFunction(IntPtr userData, IntPtr authenticationChallenge)
        {
            if (userData == IntPtr.Zero)
            {
                return;
            }
    
            var callbackObject = (ArcGISAuthenticationChallengeIssuedEventHandler)((GCHandle)userData).Target;
    
            var callback = callbackObject.m_delegate;
    
            if (callback == null)
            {
                return;
            }
    
            ArcGISAuthenticationChallenge localAuthenticationChallenge = null;
            
            if (authenticationChallenge != IntPtr.Zero)
            {
                localAuthenticationChallenge = new ArcGISAuthenticationChallenge(authenticationChallenge);
            }
            
            callback(localAuthenticationChallenge);
        }
    }
}