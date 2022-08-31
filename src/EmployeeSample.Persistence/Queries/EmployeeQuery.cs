namespace EmployeeSample.Persistence.Queries;

public static class EmployeeQuery
{
    public const string Table = "[dbo].[Employee]";

    public const string QueryInsert = @$"
        INSERT INTO {Table}(
            [Id],
            [Document],
            [FullName],
            [SocialName],
            [Sex],
            [MaritalStatus],
            [EducationLevel],
            [BirthDate],
            [Phone],
            [Email],
            [CreatedAt]
        ) VALUES (
            @Id,
            @Document,
            @FullName,
            @SocialName,
            @Sex,
            @MaritalStatus,
            @EducationLevel,
            @BirthDate,
            @Phone,
            @Email,
            @CreatedAt
        )
    ";

    public const string QueryUpdateById = @$"
        UPDATE {Table} SET 
            [Document] = @Document,
            [FullName] = @FullName,
            [SocialName] = @SocialName,
            [Sex] = @Sex,
            [MaritalStatus] = @MaritalStatus,
            [EducationLevel] = @EducationLevel,
            [BirthDate] = @BirthDate,
            [Phone] = @Phone,
            [Email] = @Email,
            [UpdatedAt] = @UpdatedAt
        WHERE [Id] = @Id
    ";

    public const string QueryDeleteById = @$"
        DELETE FROM {Table} WHERE [Id] = @Id
    ";

    public const string QueryCheckDocumentExists = @$"
        SELECT COUNT(1) 
        FROM {Table} 
        WITH (NOLOCK)
        WHERE [Document] = @Document
    ";

    public const string QueryCheckDocumentExistsById = @$"
        SELECT COUNT(1) 
        FROM {Table} 
        WITH (NOLOCK)
        WHERE [Document] = @Document
        AND [Id] <> @Id
    ";

    public const string CheckExistsById = @$"
        SELECT COUNT(1) 
        FROM {Table} 
        WITH (NOLOCK)
        WHERE [Id] = @Id
    ";

    public const string QueryGetById = @$"
        SELECT
            [Id],
            [Document],
            [FullName],
            [SocialName],
            [Sex],
            [MaritalStatus],
            [EducationLevel],
            [BirthDate],
            [Phone],
            [Email],
            [CreatedAt],
            [UpdatedAt]
        FROM {Table}
        WITH (NOLOCK)
        WHERE [Id] = @Id
    ";

    public const string QueryGetByDocument = @$"
        SELECT
            [Id],
            [Document],
            [FullName],
            [SocialName],
            [Sex],
            [MaritalStatus],
            [EducationLevel],
            [BirthDate],
            [Phone],
            [Email],
            [CreatedAt],
            [UpdatedAt]
        FROM {Table}
        WITH (NOLOCK)
        WHERE [Document] = @Document
    ";

    public const string QueryGetAll = @$"
        SELECT
            [Id],
            [Document],
            [FullName],
            [SocialName],
            [Sex],
            [MaritalStatus],
            [EducationLevel],
            [BirthDate],
            [Phone],
            [Email],
            [CreatedAt],
            [UpdatedAt]
        FROM {Table}
        WITH (NOLOCK)
        ORDER BY [FullName]
        OFFSET @Offset ROWS
        FETCH NEXT @Next ROWS ONLY
    ";
}
