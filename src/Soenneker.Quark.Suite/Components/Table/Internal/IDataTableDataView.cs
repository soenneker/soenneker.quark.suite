using System.Threading.Tasks;
namespace Soenneker.Quark.Internal;
internal interface IDataTableDataView
{
    Task Reload();
    Task SetState(QuarkViewState state);
    Task GoToPage(int page);
    void CancelLoad();
}
