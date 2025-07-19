using System.ComponentModel.DataAnnotations;

namespace book_api.Interface.Input
{
    public record BookInput(string Title, DateTime PublishDate, List<AuthorInput> Authors);


}
