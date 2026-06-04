using System;
using System.Collections.Generic;
using System.Text;

namespace HelpDeskAPI.Application.Interfaces;

public interface IDBContext
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
