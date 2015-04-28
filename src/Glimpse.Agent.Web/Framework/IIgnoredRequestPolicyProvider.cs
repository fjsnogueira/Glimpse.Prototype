﻿using System.Collections.Generic;

namespace Glimpse.Agent.Web
{
    public interface IIgnoredRequestPolicyProvider
    {
        IEnumerable<IRequestIgnorePolicy> Policies { get; }
    }
}