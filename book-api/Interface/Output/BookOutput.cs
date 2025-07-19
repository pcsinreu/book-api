namespace book_api.Interface.Output
{
    public record BookOutput(Guid Id, string Title, DateTime PublishDate, List<AuthorOutput> Authors);


}
