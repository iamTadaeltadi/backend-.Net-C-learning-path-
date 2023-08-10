
# BlogApp

Welcome to BlogApp, a versatile web application designed for creating, managing, and sharing blog posts and comments. Built with ASP.NET Core and Entity Framework Core and used EFCore to integrate PostgreSQL with .Net development, this application provides a seamless experience for both content creators and readers.


## Table of Contents
1. [Features](#features)
2. [Endpoints](#endpoints)

## Features
Users can add their posts and can also comment on blog posts.

 PostsController.cs

## Endpoints
The end-points of this application can be generally divided into two categories: [Post](#1-post) and [Comment](#2-comment).
The end-points of this api are based on the REST architecture. The request and response formats are in JSON. They are described below. Then the end points will follow:


## BlogManager Endpoints

#### Request Format
```js
{
    "title": "string",
    "content": "string",
}
```

#### Response Format
```js
{
  "postId": 123,
  "title": "New Blog Post",
  "content": "This is the content of the new blog post.",
  "comments": []
}
```
### GET /api/BlogManager/posts/{postId}
Get details of a specific blog post by its ID.

### PUT /api/BlogManager/posts/{postId}
Update a specific blog post by its ID.

### DELETE /api/BlogManager/posts/{postId}
Delete a specific blog post by its ID.

## CommentManager Endpoints

***request Format***
```js
{
  "text": "This is a new comment for the blog post."
}
```
***response Format***
```js
{

  "commentId": 456,
  "text": "This is a new comment for the blog post."
}
```

### GET /api/CommentManager/posts/{postId}/comments
Get a list of comments for a specific blog post.


### POST /api/CommentManager/posts/{postId}/comments
Create a new comment for a specific blog post.


### GET /api/CommentManager/comments/{commentId}

Get details of a specific comment by its ID.


### PUT /api/CommentManager/comments/{commentId}

Update a specific comment by its ID.


### DELETE /api/CommentManager/comments/{commentId}

Delete a specific comment by its ID.
