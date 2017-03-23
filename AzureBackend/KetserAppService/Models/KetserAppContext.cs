using System.Data.Entity;
using KetserAppService.DataObjects;

namespace KetserAppService.Models
{
    public class KetserAppContext : DbContext
    {
        // You can add custom code to this file.    Changes will not be overwritten.
        // 
        // If you want Entity Framework to alter your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        private const string connectionStringName = "Name=MS_TableConnectionString";

        public KetserAppContext() : base(connectionStringName)
        {
        } 

        public DbSet<TodoItem> TodoItems { get; set; }
        //comments
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Add(
        //        new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
        //            "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        //}    

        public System.Data.Entity.DbSet<AppserAppService.DataObjects.CEGuy> CEGuys { get; set; }

        public System.Data.Entity.DbSet<AppserAppService.DataObjects.ServiceCategory> ServiceCategories { get; set; }
    }

}
