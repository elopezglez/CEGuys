using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using AppserAppService.DataObjects;
using KetserAppService.Models;

namespace KetserAppService.Controllers
{
    public class CEGuyController : TableController<CEGuy>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            KetserAppContext context = new KetserAppContext();
            DomainManager = new EntityDomainManager<CEGuy>(context, Request);
        }

        // GET tables/CEGuy
        public IQueryable<CEGuy> GetAllCEGuy()
        {
            return Query(); 
        }

        // GET tables/CEGuy/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<CEGuy> GetCEGuy(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/CEGuy/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<CEGuy> PatchCEGuy(string id, Delta<CEGuy> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/CEGuy
        public async Task<IHttpActionResult> PostCEGuy(CEGuy item)
        {
            CEGuy current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/CEGuy/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteCEGuy(string id)
        {
             return DeleteAsync(id);
        }
    }
}
