using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineReview.Migrations
{
    /// <inheritdoc />
    public partial class refactDescricaoAndAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Comentario: prefer rename; if both exist, drop old; if neither exist, add new
            migrationBuilder.Sql(@"
                IF COL_LENGTH('SerieFilmes','Comentario') IS NULL AND COL_LENGTH('SerieFilmes','Descricao') IS NOT NULL
                BEGIN
                    EXEC sp_rename 'SerieFilmes.Descricao', 'Comentario', 'COLUMN';
                END
                ELSE IF COL_LENGTH('SerieFilmes','Comentario') IS NULL AND COL_LENGTH('SerieFilmes','Descricao') IS NULL
                BEGIN
                    ALTER TABLE SerieFilmes ADD Comentario NVARCHAR(MAX) NULL;
                END
                ELSE IF COL_LENGTH('SerieFilmes','Comentario') IS NOT NULL AND COL_LENGTH('SerieFilmes','Descricao') IS NOT NULL
                BEGIN
                    ALTER TABLE SerieFilmes DROP COLUMN Descricao;
                END
            ");

            // Nota: prefer rename; if both exist, drop old; if neither exist, add new
            migrationBuilder.Sql(@"
                IF COL_LENGTH('SerieFilmes','Nota') IS NULL AND COL_LENGTH('SerieFilmes','Avaliacao') IS NOT NULL
                BEGIN
                    EXEC sp_rename 'SerieFilmes.Avaliacao', 'Nota', 'COLUMN';
                END
                ELSE IF COL_LENGTH('SerieFilmes','Nota') IS NULL AND COL_LENGTH('SerieFilmes','Avaliacao') IS NULL
                BEGIN
                    ALTER TABLE SerieFilmes ADD Nota FLOAT NULL;
                END
                ELSE IF COL_LENGTH('SerieFilmes','Nota') IS NOT NULL AND COL_LENGTH('SerieFilmes','Avaliacao') IS NOT NULL
                BEGIN
                    ALTER TABLE SerieFilmes DROP COLUMN Avaliacao;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse Comentario -> Descricao
            migrationBuilder.Sql(@"
                IF COL_LENGTH('SerieFilmes','Descricao') IS NULL AND COL_LENGTH('SerieFilmes','Comentario') IS NOT NULL
                BEGIN
                    EXEC sp_rename 'SerieFilmes.Comentario', 'Descricao', 'COLUMN';
                END
                ELSE IF COL_LENGTH('SerieFilmes','Descricao') IS NULL AND COL_LENGTH('SerieFilmes','Comentario') IS NULL
                BEGIN
                    ALTER TABLE SerieFilmes ADD Descricao NVARCHAR(MAX) NULL;
                END
                ELSE IF COL_LENGTH('SerieFilmes','Descricao') IS NOT NULL AND COL_LENGTH('SerieFilmes','Comentario') IS NOT NULL
                BEGIN
                    ALTER TABLE SerieFilmes DROP COLUMN Comentario;
                END
            ");

            // Reverse Nota -> Avaliacao
            migrationBuilder.Sql(@"
                IF COL_LENGTH('SerieFilmes','Avaliacao') IS NULL AND COL_LENGTH('SerieFilmes','Nota') IS NOT NULL
                BEGIN
                    EXEC sp_rename 'SerieFilmes.Nota', 'Avaliacao', 'COLUMN';
                END
                ELSE IF COL_LENGTH('SerieFilmes','Avaliacao') IS NULL AND COL_LENGTH('SerieFilmes','Nota') IS NULL
                BEGIN
                    ALTER TABLE SerieFilmes ADD Avaliacao FLOAT NULL;
                END
                ELSE IF COL_LENGTH('SerieFilmes','Avaliacao') IS NOT NULL AND COL_LENGTH('SerieFilmes','Nota') IS NOT NULL
                BEGIN
                    ALTER TABLE SerieFilmes DROP COLUMN Nota;
                END
            ");
        }
    }
}
