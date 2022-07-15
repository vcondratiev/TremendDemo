using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
