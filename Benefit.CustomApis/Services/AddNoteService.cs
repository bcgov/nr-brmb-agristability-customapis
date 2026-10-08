using Microsoft.Xrm.Sdk;

namespace Benefit.CustomApis.Services;

public sealed class AddNoteService
{
    private static readonly HashSet<string> SupportedTableLogicalNames = new(
        StringComparer.OrdinalIgnoreCase)
    {
        "account",
        "vsi_benefit",
        "vsi_participantprogramyear"
    };

    private readonly IOrganizationService organizationService;

    public AddNoteService(IOrganizationService organizationService)
    {
        this.organizationService = organizationService
            ?? throw new ArgumentNullException(nameof(organizationService));
    }

    public Guid AddNote(
        Guid recordId,
        string tableLogicalName,
        string subject,
        string noteText)
    {
        if (recordId == Guid.Empty)
        {
            throw new ArgumentException("A record ID is required.", nameof(recordId));
        }

        if (string.IsNullOrWhiteSpace(tableLogicalName)
            || !SupportedTableLogicalNames.Contains(tableLogicalName))
        {
            throw new ArgumentException(
                "The table must be account, vsi_benefit, or vsi_participantprogramyear.",
                nameof(tableLogicalName));
        }

        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("A note subject is required.", nameof(subject));
        }

        if (string.IsNullOrWhiteSpace(noteText))
        {
            throw new ArgumentException("Note text is required.", nameof(noteText));
        }

        var annotation = new Entity("annotation")
        {
            ["subject"] = subject,
            ["notetext"] = noteText,
            ["objectid"] = new EntityReference(tableLogicalName, recordId)
        };

        return organizationService.Create(annotation);
    }
}