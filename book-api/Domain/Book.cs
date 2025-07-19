using System;
using System.Collections.Generic;

namespace book_api.Domain
{
    public class Book
    {
        public Guid Id { get; }
        public string Title { get; }
        public DateTime PublishDate { get; }
        public List<Author> Authors { get; }

        public Book(Guid id, string title, DateTime publishDate, List<Author> authors)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            if (publishDate > DateTime.UtcNow)
                throw new ArgumentException("Publish date cannot be in the future.");

            if (authors == null || authors.Count == 0)
                throw new ArgumentException("At least one author is required.");

            if (HasDuplicateAuthors(authors))
                throw new ArgumentException("Authors must be unique.");

            Id = id;
            Title = title;
            PublishDate = publishDate;
            Authors = authors;
        }

        private bool HasDuplicateAuthors(List<Author> authors)
        {
            var set = new HashSet<string>();
            foreach (var author in authors)
            {
                var key = $"{author.FirstName.ToLowerInvariant()}_{author.LastName.ToLowerInvariant()}";
                if (!set.Add(key))
                    return true;
            }
            return false;
        }
    }
}