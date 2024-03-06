namespace GamesGlobal.Dal.Persistence;

public interface IGeneralUnitOfWorkFactory
{
    IGeneralUnitOfWork GetConnection();
}