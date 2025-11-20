using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineReview.Migrations
{
    public partial class AddImagemURLToSerieFilmes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add ImagemURL column only if it doesn't exist
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.SerieFilmes', 'ImagemURL') IS NULL
BEGIN
    ALTER TABLE dbo.SerieFilmes ADD ImagemURL nvarchar(500) NOT NULL CONSTRAINT DF_SerieFilmes_ImagemURL DEFAULT('')
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.SerieFilmes', 'ImagemURL') IS NOT NULL
BEGIN
    ALTER TABLE dbo.SerieFilmes DROP CONSTRAINT IF EXISTS DF_SerieFilmes_ImagemURL
    ALTER TABLE dbo.SerieFilmes DROP COLUMN ImagemURL
END
");
        }
    }
}

