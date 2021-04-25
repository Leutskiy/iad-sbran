using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sbran.CQS.Read
{
	public interface IEmployeeReadCommand
	{
		Task<IEnumerable<InvitationResult>> ExecuteAllAsync(Guid employeeId);
	}
}