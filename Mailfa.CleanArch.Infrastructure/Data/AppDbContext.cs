using System.Reflection;
using Mailfa.CleanArch.Core.ProjectAggregate.hMail;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.drive;
using Mailfa.CleanArch.Core.ProjectAggregate.Models.webMail;
using Mailfa.CleanArch.SharedKernel;
using Mailfa.CleanArch.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mailfa.CleanArch.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options,
      IDomainEventDispatcher? dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    //---- WebMail
    public DbSet<WebMail_Users> WebMail_Users => Set<WebMail_Users>();
    public DbSet<WebMail_PreSignupConfirm> WebMail_PreSignupConfirm => Set<WebMail_PreSignupConfirm>();
    public DbSet<WebMail_Groups> WebMail_Groups => Set<WebMail_Groups>();
    public DbSet<WebMail_CheckRequest> WebMail_CheckRequest => Set<WebMail_CheckRequest>();


    //--- HMail
    public DbSet<hm_accounts> hm_accounts => Set<hm_accounts>();
    public DbSet<hm_imapfolders> hm_imapfolders => Set<hm_imapfolders>();




    //--- Drive
    public DbSet<DriveFolder> DriveFolder => Set<DriveFolder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region <WebMail>
        var webUsers = modelBuilder.Entity<WebMail_Users>();
        webUsers.HasKey(x => x.User_Id);

        var preSignupConfirm = modelBuilder.Entity<WebMail_PreSignupConfirm>();
        preSignupConfirm.HasKey(x => x.PSC_Id);

        var webMailGroups = modelBuilder.Entity<WebMail_Groups>();
        webMailGroups.HasKey(x => x.Group_Id);


        var webMail_CheckRequest = modelBuilder.Entity<WebMail_CheckRequest>();
        webMail_CheckRequest.HasKey(x => x.CR_ID);


        #endregion

        #region <HMail>
        var hmAccounts = modelBuilder.Entity<hm_accounts>();
        hmAccounts.HasKey(x => x.accountid);


        var hm_imapfolders = modelBuilder.Entity<hm_imapfolders>();
        hm_imapfolders.HasKey(x => x.Folderid);

        #endregion

        #region <Drive>
        var driveFolder = modelBuilder.Entity<DriveFolder>();
        driveFolder.HasKey(x => x.DFL_ID);
        #endregion

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }
}
