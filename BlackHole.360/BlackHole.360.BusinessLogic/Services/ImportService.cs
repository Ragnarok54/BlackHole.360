using BlackHole._360.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackHole._360.BusinessLogic.Services;
public class ImportService : BaseService
{
    public ImportService(IUnitOfWork unitOfWork) : base(unitOfWork) { }


}
