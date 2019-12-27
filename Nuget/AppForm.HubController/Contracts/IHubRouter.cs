using AppForm.HubController.Models;
using System.Threading.Tasks;

namespace AppForm.HubController.Contracts
{
    public interface IHubRouter
    {
        Task<object> HandleRequest(HubRequest hubRequest);
    }
}
