﻿namespace ORM.Nine.Database.Models.Output
{
    public class JsonReturnPagination
    {
        public int? PreviousPage { set; get; }

        public int? LaterPage { set; get; }

        public int? CurrentPage { set; get; }

        public int? LastPage { set; get; }
    }
}