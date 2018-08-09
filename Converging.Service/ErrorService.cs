using Converging.Data.Infrastructure;
using Converging.Data.Repositories;
using Converging.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Service
{
    public interface IErrorService
    {
        Error Create(Error error);
        void save();
    }
    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            this._errorRepository = errorRepository;
            this._unitOfWork = unitOfWork;
        }
        public Error Create(Error error)
        {
            return this._errorRepository.Add(error);
        }

        public void save()
        {
            this._unitOfWork.Commit();
        }
    }
}
