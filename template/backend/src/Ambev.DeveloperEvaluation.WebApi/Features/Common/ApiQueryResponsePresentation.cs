﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Common
{
    public class ApiQueryResponsePresentation<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}