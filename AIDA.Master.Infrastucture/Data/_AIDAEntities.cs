namespace AIDA.Master.Infrastucture.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class _AIDAEntities : DbContext
    {
        public _AIDAEntities()
            : base("name=_AIDAEntities")
        {
        }

        public virtual DbSet<AchieveGroup> AchieveGroup { get; set; }
        public virtual DbSet<AdminOperation> AdminOperation { get; set; }
        public virtual DbSet<AdminExclusive> AdminExclusive { get; set; }
        public virtual DbSet<ASM> ASM { get; set; }
        public virtual DbSet<BUM> BUM { get; set; }
        public virtual DbSet<CallAchivementSumm> CallAchivementSumm { get; set; }
        public virtual DbSet<CallActual> CallActual { get; set; }
        public virtual DbSet<CallTarget> CallTarget { get; set; }
        public virtual DbSet<CallTargetApprovalDetail> CallTargetApprovalDetail { get; set; }
        public virtual DbSet<CallTargetApprovalHeader> CallTargetApprovalHeader { get; set; }
        public virtual DbSet<CGTarget> CGTarget { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroup { get; set; }
        public virtual DbSet<ElegibilityDetail> ElegibilityDetail { get; set; }
        public virtual DbSet<ElegibilityHeader> ElegibilityHeader { get; set; }
        public virtual DbSet<ElegibilityLevelType> ElegibilityLevelType { get; set; }
        public virtual DbSet<ElegibilityRayonType> ElegibilityRayonType { get; set; }
        public virtual DbSet<ForeCastAchivement> ForeCastAchivement { get; set; }
        public virtual DbSet<ForeCastAchivementSumm> ForeCastAchivementSumm { get; set; }
        public virtual DbSet<FSS> FSS { get; set; }
        public virtual DbSet<IncentiveType> IncentiveType { get; set; }
        public virtual DbSet<KaCab> KaCab { get; set; }
        public virtual DbSet<NSM> NSM { get; set; }
        public virtual DbSet<Parameter> Parameter { get; set; }
        public virtual DbSet<PFMaster> PFMaster { get; set; }
        public virtual DbSet<PFTarget> PFTarget { get; set; }
        public virtual DbSet<RDetail> RDetail { get; set; }
        public virtual DbSet<RHDetail> RHDetail { get; set; }
        public virtual DbSet<RHDetailBAK> RHDetailBAK { get; set; }
        public virtual DbSet<RHDetailHist> RHDetailHist { get; set; }
        public virtual DbSet<RHeader> RHeader { get; set; }
        public virtual DbSet<RHHeader> RHHeader { get; set; }
        public virtual DbSet<RHHeaderBAK> RHHeaderBAK { get; set; }
        public virtual DbSet<RHTeam> RHTeam { get; set; }
        public virtual DbSet<RTDetail> RTDetail { get; set; }
        public virtual DbSet<RTDetailHist> RTDetailHist { get; set; }
        public virtual DbSet<RTHeader> RTHeader { get; set; }
        public virtual DbSet<SalesCollection> SalesCollection { get; set; }
        public virtual DbSet<SalesGroup> SalesGroup { get; set; }
        public virtual DbSet<SalesTarget> SalesTarget { get; set; }
        public virtual DbSet<SLM> SLM { get; set; }
        public virtual DbSet<SLM_new> SLM_new { get; set; }
        public virtual DbSet<TApproval> TApproval { get; set; }
        public virtual DbSet<TClaim> TClaim { get; set; }
        public virtual DbSet<TCollector> TCollector { get; set; }
        public virtual DbSet<TCustomer> TCustomer { get; set; }
        public virtual DbSet<TFakturis> TFakturis { get; set; }
        public virtual DbSet<TFConfig> TFConfig { get; set; }
        public virtual DbSet<TFTarget> TFTarget { get; set; }
        public virtual DbSet<TModulUpload> TModulUpload { get; set; }
        public virtual DbSet<TPerm> TPerm { get; set; }
        public virtual DbSet<TransAchivementSumm> TransAchivementSumm { get; set; }
        public virtual DbSet<TransAchivementSummASM> TransAchivementSummASM { get; set; }
        public virtual DbSet<TRole> TRole { get; set; }
        public virtual DbSet<TRolePerm> TRolePerm { get; set; }
        public virtual DbSet<TSPVFakturis> TSPVFakturis { get; set; }
        public virtual DbSet<TTempSalesTarget> TTempSalesTarget { get; set; }
        public virtual DbSet<TUser> TUser { get; set; }
        public virtual DbSet<TUserRole> TUserRole { get; set; }
        public virtual DbSet<CallIncentivesASM> CallIncentivesASM { get; set; }
        public virtual DbSet<CollectionAchievement> CollectionAchievement { get; set; }
        public virtual DbSet<CollectionAchievementAR120> CollectionAchievementAR120 { get; set; }
        public virtual DbSet<CollectionAchievementAR90> CollectionAchievementAR90 { get; set; }
        public virtual DbSet<CollectionAchievementARASM> CollectionAchievementARASM { get; set; }
        public virtual DbSet<CollectionAchievementARFSS> CollectionAchievementARFSS { get; set; }
        public virtual DbSet<CollectionAchievementARNSM> CollectionAchievementARNSM { get; set; }
        public virtual DbSet<CollectionAchievementARSLM> CollectionAchievementARSLM { get; set; }
        public virtual DbSet<CollectionAchievementBQASM> CollectionAchievementBQASM { get; set; }
        public virtual DbSet<CollectionAchievementBQFSS> CollectionAchievementBQFSS { get; set; }
        public virtual DbSet<CollectionAchievementBQNSM> CollectionAchievementBQNSM { get; set; }
        public virtual DbSet<CollectionAchievementBQSLM> CollectionAchievementBQSLM { get; set; }
        public virtual DbSet<CollectionAchievementIC> CollectionAchievementIC { get; set; }
        public virtual DbSet<CollectionAchievementICASM> CollectionAchievementICASM { get; set; }
        public virtual DbSet<CollectionAchievementICFSS> CollectionAchievementICFSS { get; set; }
        public virtual DbSet<CollectionAchievementICNSM> CollectionAchievementICNSM { get; set; }
        public virtual DbSet<CollectionAchievementICSLM> CollectionAchievementICSLM { get; set; }
        public virtual DbSet<CustGorupAchivementSumm> CustGorupAchivementSumm { get; set; }
        public virtual DbSet<CustomerMap> CustomerMap { get; set; }
        public virtual DbSet<DeleteML> DeleteML { get; set; }
        public virtual DbSet<DoubleML> DoubleML { get; set; }
        public virtual DbSet<InvoceCorrectionAchievement> InvoceCorrectionAchievement { get; set; }
        public virtual DbSet<OpenBalanceMonthly> OpenBalanceMonthly { get; set; }
        public virtual DbSet<OpenBalanceMonthlyHist> OpenBalanceMonthlyHist { get; set; }
        public virtual DbSet<Plant> Plant { get; set; }
        public virtual DbSet<ProductFocusAchievementSumm> ProductFocusAchievementSumm { get; set; }
        public virtual DbSet<RDIOrder> RDIOrder { get; set; }
        public virtual DbSet<RHDetailExclude> RHDetailExclude { get; set; }
        public virtual DbSet<RHHeaderExclude> RHHeaderExclude { get; set; }
        public virtual DbSet<SalesAchievement> SalesAchievement { get; set; }
        public virtual DbSet<SalesIncentives> SalesIncentives { get; set; }
        public virtual DbSet<SalesIncentivesASM_SD1> SalesIncentivesASM_SD1 { get; set; }
        public virtual DbSet<SalesIncentivesASM_SD2> SalesIncentivesASM_SD2 { get; set; }
        public virtual DbSet<SalesIncentivesFSS> SalesIncentivesFSS { get; set; }
        public virtual DbSet<SalesRayonCustomer> SalesRayonCustomer { get; set; }
        public virtual DbSet<SalesTran> SalesTran { get; set; }
        public virtual DbSet<SalesTranExtended> SalesTranExtended { get; set; }
        public virtual DbSet<TOutlet> TOutlet { get; set; }
        public virtual DbSet<UploadHier> UploadHier { get; set; }
        public virtual DbSet<UploadHierTagih> UploadHierTagih { get; set; }
        public virtual DbSet<UploadML> UploadML { get; set; }
        public virtual DbSet<UploadML2> UploadML2 { get; set; }
        //public virtual DbSet<UploadCollection> UploadCollection { get; set; }
        public virtual DbSet<UploadHier2> UploadHier2 { get; set; }
        public virtual DbSet<UploadHierTagih2> UploadHierTagih2 { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            Database.SetInitializer<_AIDAEntities>(null);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AchieveGroup>()
                .Property(e => e.AchiGroup)
                .IsUnicode(false);

            modelBuilder.Entity<AchieveGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<AchieveGroup>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<AchieveGroup>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ASM>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<ASM>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ASM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ASM>()
                .Property(e => e.DefaultRayonType)
                .IsUnicode(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.ASMObj1)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHHeader1)
                .WithRequired(e => e.ASMObj2)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHTeam)
                .WithRequired(e => e.ASM1)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHTeam1)
                .WithRequired(e => e.ASM2)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHTeam2)
                .WithRequired(e => e.ASM3)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ASM>()
                .HasMany(e => e.RHTeam3)
                .WithRequired(e => e.ASM4)
                .HasForeignKey(e => e.ASM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<BUM>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<BUM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.BUMObj1)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHHeader1)
                .WithRequired(e => e.BUMObj2)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHTeam)
                .WithRequired(e => e.BUM1)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHTeam1)
                .WithRequired(e => e.BUM2)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHTeam2)
                .WithRequired(e => e.BUM3)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BUM>()
                .HasMany(e => e.RHTeam3)
                .WithRequired(e => e.BUM4)
                .HasForeignKey(e => e.BUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CallAchivementSumm>()
                .Property(e => e.RAYONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<CallAchivementSumm>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.ReasonDesc)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.RecordStatus)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.ApprovedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallActual>()
                .Property(e => e.ApprovedStatus)
                .IsUnicode(false);

            modelBuilder.Entity<CallTarget>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CallTarget>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<CallTarget>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallTarget>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalDetail>()
                .Property(e => e.HeaderID)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalDetail>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalDetail>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalDetail>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalHeader>()
                .Property(e => e.HeaderID)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalHeader>()
                .Property(e => e.UploadedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalHeader>()
                .Property(e => e.ApprovedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalHeader>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<CallTargetApprovalHeader>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<CGTarget>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CGTarget>()
                .Property(e => e.CustGroup)
                .IsUnicode(false);

            modelBuilder.Entity<CGTarget>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.CustGroup)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.CustName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerGroup>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityDetail>()
                .Property(e => e.ElegID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityDetail>()
                .Property(e => e.TierType)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityDetail>()
                .Property(e => e.CalculationType)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityHeader>()
                .Property(e => e.ElegID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityHeader>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityHeader>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityHeader>()
                .Property(e => e.CalculationType)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityLevelType>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityLevelType>()
                .Property(e => e.ElegID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityLevelType>()
                .Property(e => e.IncTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityLevelType>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityLevelType>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityRayonType>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityRayonType>()
                .Property(e => e.ElegID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityRayonType>()
                .Property(e => e.IncTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<ElegibilityRayonType>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivement>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivement>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivement>()
                .Property(e => e.FSS)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivement>()
                .Property(e => e.ForeCastAchieve)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ForeCastAchivement>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.FSS)
                .IsUnicode(false);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.ForeCastAchieve)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.ForecastTarget)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ForeCastAchivementSumm>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<FSS>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<FSS>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FSS>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<FSS>()
                .Property(e => e.DefaultRayonType)
                .IsUnicode(false);

            modelBuilder.Entity<FSS>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.FSSObj1)
                .HasForeignKey(e => e.FSS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FSS>()
                .HasMany(e => e.RHHeader1)
                .WithRequired(e => e.FSSObj2)
                .HasForeignKey(e => e.FSS)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FSS>()
                .HasMany(e => e.TFConfig)
                .WithRequired(e => e.FSS)
                .HasForeignKey(e => e.NIK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FSS>()
                .HasMany(e => e.TFConfig1)
                .WithRequired(e => e.FSS1)
                .HasForeignKey(e => e.NIK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncentiveType>()
                .Property(e => e.IncTypeID)
                .IsUnicode(false);

            modelBuilder.Entity<IncentiveType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<IncentiveType>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<NSM>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<NSM>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<NSM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.NSMObj1)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHHeader1)
                .WithRequired(e => e.NSMObj2)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHTeam)
                .WithRequired(e => e.NSM1)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHTeam1)
                .WithRequired(e => e.NSM2)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHTeam2)
                .WithRequired(e => e.NSM3)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NSM>()
                .HasMany(e => e.RHTeam3)
                .WithRequired(e => e.NSM4)
                .HasForeignKey(e => e.NSM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.ParamID)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.ParamDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.ParamValue)
                .IsUnicode(false);

            modelBuilder.Entity<Parameter>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PFMaster>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<PFMaster>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<PFMaster>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PFMaster>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<PFTarget>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<PFTarget>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<PFTarget>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<PFTarget>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.AchiGroup)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RDetail>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetail>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetail>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetail>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetail>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailBAK>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailBAK>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailBAK>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailBAK>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailHist>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailHist>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailHist>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailHist>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .Property(e => e.SalesGroup)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHeader>()
                .HasMany(e => e.RDetail)
                .WithRequired(e => e.RHeader)
                .HasForeignKey(e => e.RayonType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHeader>()
                .HasMany(e => e.RDetail1)
                .WithRequired(e => e.RHeader1)
                .HasForeignKey(e => e.RayonType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHeader>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.RHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RHHeader>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeader>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeader>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeader>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeader>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderBAK>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderBAK>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderBAK>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderBAK>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderBAK>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHTeam>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHTeam>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHTeam>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetail>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetail>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetail>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetail>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetailHist>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetailHist>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetailHist>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTDetailHist>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTHeader>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RTHeader>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<RTHeader>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RTHeader>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesCollection>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<SalesCollection>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<SalesCollection>()
                .Property(e => e.Group2)
                .IsUnicode(false);

            modelBuilder.Entity<SalesCollection>()
                .Property(e => e.Group1)
                .IsUnicode(false);

            modelBuilder.Entity<SalesCollection>()
                .Property(e => e.Channel)
                .IsUnicode(false);

            modelBuilder.Entity<SalesGroup>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<SalesGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SalesGroup>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesGroup>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesGroup>()
                .HasMany(e => e.RHeader)
                .WithRequired(e => e.SalesGroup1)
                .HasForeignKey(e => e.SalesGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesGroup>()
                .HasMany(e => e.RHeader1)
                .WithRequired(e => e.SalesGroup2)
                .HasForeignKey(e => e.SalesGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesGroup>()
                .HasMany(e => e.RHeader2)
                .WithRequired(e => e.SalesGroup3)
                .HasForeignKey(e => e.SalesGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesTarget>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTarget>()
                .Property(e => e.AchiGroup)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTarget>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTarget>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTarget>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<SLM>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<SLM>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SLM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHHeader)
                .WithRequired(e => e.SLMObj1)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHHeader1)
                .WithRequired(e => e.SLMObj2)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHTeam)
                .WithRequired(e => e.SLM1)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHTeam1)
                .WithRequired(e => e.SLM2)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHTeam2)
                .WithRequired(e => e.SLM3)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM>()
                .HasMany(e => e.RHTeam3)
                .WithRequired(e => e.SLM4)
                .HasForeignKey(e => e.SLM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SLM_new>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<SLM_new>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SLM_new>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SLM_new>()
                .Property(e => e.FullName_new)
                .IsUnicode(false);

            modelBuilder.Entity<TApproval>()
                .Property(e => e.ApprID)
                .IsUnicode(false);

            modelBuilder.Entity<TApproval>()
                .Property(e => e.RSLM)
                .IsUnicode(false);

            modelBuilder.Entity<TApproval>()
                .Property(e => e.RFSS)
                .IsUnicode(false);

            modelBuilder.Entity<TApproval>()
                .Property(e => e.RASM)
                .IsUnicode(false);

            modelBuilder.Entity<TApproval>()
                .Property(e => e.RNSM)
                .IsUnicode(false);

            modelBuilder.Entity<TClaim>()
                .Property(e => e.ClaimType)
                .IsUnicode(false);

            modelBuilder.Entity<TClaim>()
                .Property(e => e.ClaimValue)
                .IsUnicode(false);

            modelBuilder.Entity<TCollector>()
                .Property(e => e.FULLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<TCollector>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TCollector>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            //modelBuilder.Entity<TCollector>()
            //    .HasMany(e => e.RTHeader)
            //    .WithRequired(e => e.CollectorObj)
            //    .HasForeignKey(e => e.Collector)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.IndCode1)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.IndCode2)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.IndCode3)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.IndCode4)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<TCustomer>()
                .Property(e => e.SalesOffice)
                .IsUnicode(false);

            modelBuilder.Entity<TFakturis>()
                .Property(e => e.FULLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<TFakturis>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TFakturis>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            //modelBuilder.Entity<TFakturis>()
            //    .HasMany(e => e.RTHeader)
            //    .WithRequired(e => e.FakturisObj)
            //    .HasForeignKey(e => e.Fakturis)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TFConfig>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<TFConfig>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<TFConfig>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<TFConfig>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TFConfig>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TFConfig>()
                .HasMany(e => e.TFTarget)
                .WithRequired(e => e.TFConfig)
                .HasForeignKey(e => new { e.NIK, e.Plant, e.Division })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TFConfig>()
                .HasMany(e => e.TFTarget1)
                .WithRequired(e => e.TFConfig1)
                .HasForeignKey(e => new { e.NIK, e.Plant, e.Division })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TFConfig>()
                .HasMany(e => e.TFTarget2)
                .WithRequired(e => e.TFConfig2)
                .HasForeignKey(e => new { e.NIK, e.Plant, e.Division })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TFConfig>()
                .HasMany(e => e.TFTarget3)
                .WithRequired(e => e.TFConfig3)
                .HasForeignKey(e => new { e.NIK, e.Plant, e.Division })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TFTarget>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<TFTarget>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<TFTarget>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TModulUpload>()
                .Property(e => e.ApprID)
                .IsUnicode(false);

            modelBuilder.Entity<TModulUpload>()
                .Property(e => e.ApprName)
                .IsUnicode(false);

            modelBuilder.Entity<TPerm>()
                .Property(e => e.MenuName)
                .IsUnicode(false);

            modelBuilder.Entity<TPerm>()
                .Property(e => e.MenuTag)
                .IsUnicode(false);

            modelBuilder.Entity<TPerm>()
                .Property(e => e.ContName)
                .IsUnicode(false);

            modelBuilder.Entity<TPerm>()
                .Property(e => e.ActName)
                .IsUnicode(false);

            modelBuilder.Entity<TransAchivementSumm>()
                .Property(e => e.RAYONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<TransAchivementSumm>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<TransAchivementSummASM>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<TRole>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<TRole>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<TSPVFakturis>()
                .Property(e => e.FULLNAME)
                .IsUnicode(false);

            modelBuilder.Entity<TSPVFakturis>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TSPVFakturis>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            //modelBuilder.Entity<TSPVFakturis>()
            //    .HasMany(e => e.RTHeader)
            //    .WithRequired(e => e.SPVFakturisObj)
            //    .HasForeignKey(e => e.SPVFakturis)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<TTempSalesTarget>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<TTempSalesTarget>()
                .Property(e => e.AchiGroup)
                .IsUnicode(false);

            modelBuilder.Entity<TTempSalesTarget>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<TTempSalesTarget>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<TTempSalesTarget>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<TUser>()
                .Property(e => e.PasswordHash)
                .IsUnicode(false);

            modelBuilder.Entity<TUser>()
                .Property(e => e.SecurityStamp)
                .IsUnicode(false);

            modelBuilder.Entity<TUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<CallIncentivesASM>()
                .Property(e => e.ASM_Name)
                .IsUnicode(false);

            modelBuilder.Entity<CallIncentivesASM>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievement>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementAR120>()
                .Property(e => e.st_NSM)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementAR120>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementAR120>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementAR90>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementAR90>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARASM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARASM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARFSS>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARFSS>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARNSM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARNSM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARSLM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementARSLM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQASM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQASM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQFSS>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQFSS>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQNSM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQNSM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQSLM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementBQSLM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementIC>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementIC>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICASM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICASM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICFSS>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICFSS>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICNSM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICNSM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICSLM>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CollectionAchievementICSLM>()
                .Property(e => e.Elegibility)
                .IsUnicode(false);

            modelBuilder.Entity<CustGorupAchivementSumm>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<CustGorupAchivementSumm>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<CustGorupAchivementSumm>()
                .Property(e => e.CustGroup)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMap>()
                .Property(e => e.OldCustomer)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMap>()
                .Property(e => e.NewCustomer)
                .IsUnicode(false);

            modelBuilder.Entity<DoubleML>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<DoubleML>()
                .Property(e => e.SLMName)
                .IsUnicode(false);

            modelBuilder.Entity<DoubleML>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<DoubleML>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<InvoceCorrectionAchievement>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.REFERENCE)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.CUSTOMER)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.CG1)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.PH3)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.MATERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.AMOUNT_09)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OpenBalanceMonthly>()
                .Property(e => e.INTERV)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.REFERENCE)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.CUSTOMER)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.CG1)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.PH3)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.MATERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.AMOUNT_09)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OpenBalanceMonthlyHist>()
                .Property(e => e.INTERV)
                .IsUnicode(false);

            modelBuilder.Entity<Plant>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductFocusAchievementSumm>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<ProductFocusAchievementSumm>()
                .Property(e => e.UpdateBy)
                .IsUnicode(false);

            modelBuilder.Entity<RDIOrder>()
                .Property(e => e.SalesOrder)
                .IsUnicode(false);

            modelBuilder.Entity<RDIOrder>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RDIOrder>()
                .Property(e => e.OrderStatus)
                .IsUnicode(false);

            modelBuilder.Entity<RDIOrder>()
                .Property(e => e.OFCT)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailExclude>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailExclude>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailExclude>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHDetailExclude>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderExclude>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderExclude>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderExclude>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderExclude>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<RHHeaderExclude>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesAchievement>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesAchievement>()
                .Property(e => e.Group)
                .IsUnicode(false);

            modelBuilder.Entity<SalesAchievement>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentives>()
                .Property(e => e.RAYONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentives>()
                .Property(e => e.Group)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentives>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentivesASM_SD1>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentivesASM_SD2>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentivesASM_SD2>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentivesFSS>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesIncentivesFSS>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.RAYONCODE)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.CUSTOMER)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.GROUP)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.DIVISION)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.MATERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.UOM)
                .IsUnicode(false);

            modelBuilder.Entity<SalesRayonCustomer>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.SalesOrder)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Invoice)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Division)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Material)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Total)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.PrincipalDiscount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.APLDiscount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.UOM)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTran>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.SALESORDER)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.INVOICE)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.PLANT)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.CUSTOMER)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.DIVISION)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.MATERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.TOTAL)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.PRINCIPALDISCOUNT)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.APLDISCOUNT)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.UOM)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.AchiGroup)
                .IsUnicode(false);

            modelBuilder.Entity<SalesTranExtended>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.SLM_Name)
                .IsUnicode(false);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.TotalSales)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TOutlet>()
                .Property(e => e.TotalReturn)
                .HasPrecision(18, 0);

            modelBuilder.Entity<UploadHier>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHier>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHier>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHierTagih>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHierTagih>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<UploadML>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadML>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<UploadML2>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadML2>()
                .Property(e => e.Customer)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHier2>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHier2>()
                .Property(e => e.Plant)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHier2>()
                .Property(e => e.RayonType)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHierTagih2>()
                .Property(e => e.RayonCode)
                .IsUnicode(false);

            modelBuilder.Entity<UploadHierTagih2>()
                .Property(e => e.Plant)
                .IsUnicode(false);
        }
    }
}
