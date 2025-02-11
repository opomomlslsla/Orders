using Application.Commands;
using Application.Commands.Carts;
using Application.Commands.Carts.Handlers;
using Application.Commands.Orders;
using Application.Commands.Orders.Handlers;
using Application.Commands.Products;
using Application.Commands.Products.Handlers;
using Application.Commands.Users;
using Application.Commands.Users.Handlers;
using Application.DTO;
using Application.Queries.Carts;
using Application.Queries.Carts.Handlers;
using Application.Queries.Orders;
using Application.Queries.Orders.Handlers;
using Application.Queries.Products;
using Application.Queries.Products.Handlers;
using Application.Queries.Users;
using Application.Queries.Users.Handlers;
using Application.Tools;
using Application.Validation;
using Domain.Interfaces;
using Domain.Interfaces.Commands;
using Domain.Interfaces.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public  static class Entry
{
    public static IServiceCollection ConfigureApplicationsServices(this IServiceCollection services)
    {
        services.AddScoped<IJWTProvider, JWTProvider>();
        services.AddScoped<ICommandHandler<ConfirmOrderCommand, Result<string>>, ConfirmOrderCommandHandler>();
        services.AddScoped<ICommandHandler<CompleteOrderCommand, Result<string>>, CompleteOrderCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteOrderCommand, Result<string>>, DeleteOrderCommandHandler>();
        services.AddScoped<ICommandHandler<AddOrderCommand, Result<OrderDTO>>, AddOrderCommandHandler>();
        services.AddScoped<ICommandHandler<AddToCartCommand, Result<string>>, AddToCartCommandHandler>();
        services.AddScoped<ICommandHandler<RemoveFromCartCommand, Result<string>>, RemoveFromCartCommandHandler>();
        services.AddScoped<ICommandHandler<AddProductCommand, Result<ProductDTO>>, AddProductCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateProductCommand, Result<ProductDTO>>, UpdateProductCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteProductCommand, Result<string>>, DeleteProductCommandHandler>();
        services.AddScoped<ICommandHandler<RegisterCommand, Result<UserDTO>>, RegisterCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteUserCommand, Result<string>>, DeleteUserCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateUserCommand, Result<UserDTO>>, UpdateUserCommandHandler>();
        services.AddScoped<IQueryHandler<GetMyCartQuery, Result<CartDTO>>, GerMyCartQueryHandler>();
        services.AddScoped<IQueryHandler<GetMyOrdersQuery, Result<List<OrderDTO>>>, GetMyOrdersQueryHandler>();
        services.AddScoped<IQueryHandler<GetProductsQuery, Result<List<ProductDTO>>>, GetProductsQueryHanldler>();
        services.AddScoped<IQueryHandler<GetProductQuery, Result<ProductDTO>>, GetProductQueryHandler>();
        services.AddScoped<IQueryHandler<GetUserQuery, Result<UserDTO>>, GetUserQueryHandler>();
        services.AddScoped<IQueryHandler<GetUsersQuery, Result<List<UserDTO>>>, GetUsersQueryHandler>();
        services.AddScoped<IQueryHandler<LoginQuery, Result<LoginDTO>>, LoginQueryHandler>();
        services.AddScoped<IQueryHandler<GetOrdersQuery, Result<List<OrderDTO>>>, GetOrdersQueryHandler>();
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddScoped<IValidator<AddToCartCommand>, AddToCartCommandValidator>();
        services.AddScoped<IValidator<RemoveFromCartCommand>, RemoveFromCartCommandValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<LoginQuery>, LoginQueryValidation>();
        services.AddScoped<IValidator<GetOrdersQuery>, GetOrdersQueryValidator>();
        services.AddScoped<IValidator<GetProductsQuery>, GetProductsQueryValidator>();
        return services;
    }
}