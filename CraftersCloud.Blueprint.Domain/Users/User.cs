﻿using CraftersCloud.Blueprint.Core.Entities;
using CraftersCloud.Blueprint.Domain.Authorization;
using CraftersCloud.Blueprint.Domain.Companies;
using CraftersCloud.Blueprint.Domain.Users.Commands;
using CraftersCloud.Blueprint.Domain.Users.DomainEvents;

namespace CraftersCloud.Blueprint.Domain.Users;

public class User : EntityWithCreatedUpdated
{
    public const int NameMaxLength = 200;
    public const int EmailAddressMaxLength = 200;
    public static readonly Guid SystemUserId = new("DFB44AA8-BFC9-4D95-8F45-ED6DA241DCFC");
    public string EmailAddress { get; private set; } = string.Empty;
    public string FullName { get; private set; } = string.Empty;
    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = null!;
    public UserStatusId UserStatusId { get; private set; } = null!;
    public UserStatus UserStatus { get; private set; } = null!;
    public Guid? CompanyId { get; private set; }
    public Company? Company { get; private set; }

    private readonly IList<UserCompanyHistories> _userCompanyHistories = [];
    public ICollection<UserCompanyHistories> UserCompanyHistories => _userCompanyHistories;

    public static User Create(CreateOrUpdateUser.Command command)
    {
        var result = new User
        {
            EmailAddress = command.EmailAddress, FullName = command.FullName, RoleId = command.RoleId, UserStatusId = command.UserStatusId, CompanyId = command.CompanyId
        };

        if (result.CompanyId != null) result.UpdateUserCompanyHistories(command.CompanyId);
        result.AddDomainEvent(new UserCreatedDomainEvent(result.EmailAddress));
        return result;
    }

    public void Update(CreateOrUpdateUser.Command command)
    {
        if (CompanyId != command.CompanyId) UpdateUserCompanyHistories(command.CompanyId);
        FullName = command.FullName;
        RoleId = command.RoleId;
        UserStatusId = command.UserStatusId;
        CompanyId = command.CompanyId;
        AddDomainEvent(new UserUpdatedDomainEvent(EmailAddress));
    }

    public void UpdateRole(Role role)
    {
        RoleId = role.Id;
        Role = role;
        AddDomainEvent(new UserUpdatedDomainEvent(EmailAddress));
    }

    private void UpdateUserCompanyHistories(Guid? companyId) => UserCompanyHistories.Add(new UserCompanyHistories()
    {
        CompanyId = companyId,
        UserId = Id,
        EnrollmentDate = DateOnly.FromDateTime(DateTime.Now),
        EnrollmentDateTime = DateTimeOffset.Now
    });

    public IReadOnlyCollection<PermissionId> GetPermissionIds() => Role.Permissions.Select(p => p.Id).ToArray();
}