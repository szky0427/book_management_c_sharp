using Microsoft.EntityFrameworkCore;
using book_management_c_sharp.Models;

namespace book_management_c_sharp.Data
{
    // Entity Framework がDBとの通信を管理するためのクラス
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        // プロパティを追加することでDB内のテーブルにアクセスできる
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .ToTable("books")
            .Property(b => b.Id)
            .HasColumnName("id");

            modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .HasColumnName("title");

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnName("price");

            modelBuilder.Entity<Book>()
                .Property(b => b.PublishStatus)
                .HasColumnName("publish_status");

            modelBuilder.Entity<Author>()
            .ToTable("authors") // テーブル名を小文字に
            .Property(a => a.Id)
            .HasColumnName("id");

            modelBuilder.Entity<Author>()
                .Property(a => a.Name)
                .HasColumnName("name");

            modelBuilder.Entity<Author>()
                .Property(a => a.BirthDate)
                .HasColumnName("birth_date");

            // 複合キーの登録
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>()
            .Property(ba => ba.BookId)
            .HasColumnName("book_id");

            modelBuilder.Entity<BookAuthor>()
                .Property(ba => ba.AuthorId)
                .HasColumnName("author_id");

            // BookとBookAuthorのリレーションの設定
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book) // BookAuthor(子)は1つのBook(親)に関連付けられている
                .WithMany(b => b.BookAuthors) // Book(親)は複数のBookAuthor(子)に関連付けられている。Book.BookAuthorsで表現
                .HasForeignKey(ba => ba.BookId); // 外部キーを登録

            // AuthorとBookAuthorのリレーションの設定
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author) // BookAuthor(子)は1つのAuthor(親)に関連付けられている
                .WithMany() // Author(親)は複数のBookAuthor(子)に関連付けられている。
                .HasForeignKey(ba => ba.AuthorId); // 外部キーを登録
        }
    }
}
