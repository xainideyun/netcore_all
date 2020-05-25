using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HT.Future.Common
{
    public interface ISeed
    {
        void SetSeed(ModelBuilder modelBuilder);
    }
}
