﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo_ef
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GMS_WorkOrder> GMS_WorkOrder { get; set; }
        public virtual DbSet<GMS_WorkOrderEquipment> GMS_WorkOrderEquipment { get; set; }
        public virtual DbSet<GMS_WorkOrderEquipmentAttachment> GMS_WorkOrderEquipmentAttachment { get; set; }
        public virtual DbSet<GMS_WorkOrderPoints> GMS_WorkOrderPoints { get; set; }
    }
}