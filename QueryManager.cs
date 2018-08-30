using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentDBTodo.Models;

namespace DocumentDBTodo
{
    public partial class QueryManager
    {
        static QueryManager defaultInstance = new QueryManager();

        const string accountURL = @"https://stadiyumdb.documents.azure.com:443/";
        const string accountKey = @"NOIMnV5NeYZmDnT7KEjvSHnMpW11NKPshMJ5vwhtgGY5OvWmP98OzfWILP8sIBWR8EMlWcV7IInqxSlF0amcvQ==";
        const string databaseId = @"StadiYUMDB";
        const string userCollection = @"Users";
        const string orderCollection = @"Orders";
        const string concessionCollection = @"ConcessionStand";
        const string itemCollection = @"Food";
        const string stadiumCollection = @"Stadium";

        private Uri UserLink = UriFactory.CreateDocumentCollectionUri(databaseId, userCollection);
        private Uri OrderLink = UriFactory.CreateDocumentCollectionUri(databaseId, orderCollection);
        private Uri ConcessionLink = UriFactory.CreateDocumentCollectionUri(databaseId, concessionCollection);
        private Uri ItemLink = UriFactory.CreateDocumentCollectionUri(databaseId, itemCollection);
        private Uri SadiumLink = UriFactory.CreateDocumentCollectionUri(databaseId, stadiumCollection);

        private DocumentClient client;

        public QueryManager()
        {
            client = new DocumentClient(new System.Uri(accountURL), accountKey);
        }

        public static QueryManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }
        public bool LoginEntry(User newUser)
        {
            var ExistingUser = client.CreateDocumentQuery<User>(UserLink, new FeedOptions { MaxItemCount = 1 }).Where(u=>u.Username==newUser.Username).AsEnumerable().FirstOrDefault();
            if (ExistingUser == null)
            {
                App.currentUser = newUser;
                return true;
            }
            else if (ExistingUser.Username == newUser.Username && ExistingUser.Password == newUser.Password)
            {
                App.currentUser= newUser;
                return true;
            }
            else if (ExistingUser.Username == newUser.Username && ExistingUser.Password != newUser.Password)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        public async Task SeatEnteredAsync(User thisUser)
        {
            var ExistingUser = client.CreateDocumentQuery<User>(UserLink, new FeedOptions { MaxItemCount = 1 }).Where(u => u.Username == App.currentUser.Username).AsEnumerable().FirstOrDefault();
            ExistingUser.CurrentSection = thisUser.CurrentSection;
            ExistingUser.Seat = thisUser.Seat;
            var update = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, userCollection, ExistingUser.Id), ExistingUser);

        }
        public async Task<List<Concession>> GetConcessions()
        {
            var theseConcessions = client.CreateDocumentQuery(ConcessionLink, new FeedOptions { MaxItemCount = -1 }).AsDocumentQuery();
            List<Concession> newList = new List<Concession>();
            while (theseConcessions.HasMoreResults)
            {
                newList.AddRange(await theseConcessions.ExecuteNextAsync<Concession>());
            }
            return newList;
        }
        //public List<TodoItem> Items { get; private set; }

        //public async Task<List<TodoItem>> GetTodoItemsAsync()
        //{
        //    try
        //    {
        //        // The query excludes completed TodoItems
        //        var query = client.CreateDocumentQuery<TodoItem>(collectionLink, new FeedOptions { MaxItemCount = -1 })
        //              .Where(todoItem => todoItem.Complete == false)
        //              .AsDocumentQuery();

        //        Items = new List<TodoItem>();
        //        while (query.HasMoreResults)
        //        {
        //            Items.AddRange(await query.ExecuteNextAsync<TodoItem>());
        //        }


        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //        return null;
        //    }

        //    return Items;
        //}

        //public async Task<TodoItem> InsertItemAsync(TodoItem todoItem)
        //{
        //    try
        //    {
        //        var result = await client.CreateDocumentAsync(collectionLink, todoItem);
        //        todoItem.Id = result.Resource.Id;
        //        Items.Add(todoItem);


        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //    return todoItem;
        //}

        //public async Task CompleteItemAsync(TodoItem item)
        //{
        //    try
        //    {
        //        item.Complete = true;
        //        await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseId, collectionId, item.Id), item);

        //        Items.Remove(item);

        //    }
        //    catch (Exception e)
        //    {
        //        Console.Error.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}
    }
}

