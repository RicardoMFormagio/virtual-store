// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using VShop.IdentityServer.MainModule.Consent;

namespace VShop.IdentityServer.MainModule.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}