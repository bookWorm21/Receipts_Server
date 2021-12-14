﻿using Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receipts_Server.Services.Interfaces
{
    public interface IOwnerInfoService
    {
        OwnerInfo GetOwnerInfo(int ownerId);
    }
}