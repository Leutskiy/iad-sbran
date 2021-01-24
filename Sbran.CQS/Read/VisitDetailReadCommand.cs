using Sbran.CQS.Converters;
using Sbran.CQS.Read.Contracts;
using Sbran.CQS.Read.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sbran.Domain.Data.Repositories;

namespace Sbran.CQS.Read
{
	/// <summary>
	/// Команда чтения деталей визита
	/// </summary>
	public sealed class VisitDetailReadCommand : IReadCommand<VisitDetailResult>
    {
        private readonly IVisitDetailRepository _visitDetailRepository;

        public VisitDetailReadCommand(IVisitDetailRepository visitDetailRepository)
        {
            _visitDetailRepository = visitDetailRepository;
        }

        /// <summary>
        /// Выполнить команду
        /// </summary>
        /// <param name="visitDetailId">Идентификатор деталей визита</param>
        /// <returns>Информация о деталях визита</returns>
        public async Task<VisitDetailResult> ExecuteAsync(Guid visitDetailId)
        {
            var visitDetail = await _visitDetailRepository.GetAsync(visitDetailId);

            return DomainEntityConverter.ConvertToResult(visitDetail: visitDetail);
        }

        public Task<IEnumerable<VisitDetailResult>> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}