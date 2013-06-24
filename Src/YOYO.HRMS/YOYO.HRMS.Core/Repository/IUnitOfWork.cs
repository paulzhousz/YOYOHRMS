
namespace YOYO.HRMS.Core.Repository
{
    public interface IUnitOfWork
    {
        void StartNew();
        void Commit();
        void Rollback();
    }
}
