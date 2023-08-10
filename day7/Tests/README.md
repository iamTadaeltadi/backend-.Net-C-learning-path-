
Project Tests: BlogManager CRUD Operations
This project includes a suite of unit tests that validate the CRUD (Create, Read, Update, Delete) operations for the Post and Comment entities in the BlogManager class. The tests ensure the correctness of these operations within an in-memory database environment.

Test Descriptions
The unit tests cover the following CRUD operations for both Post and Comment entities:

Data Setup: The tests create an instance of ApiDbContext with an in-memory database for testing purposes. They insert sample Post and Comment entities into the database using the BlogManager class.

Data Retrieval: The tests retrieve entities using the various retrieval methods (GetAllPostsAsync, GetPostByIdAsync, GetAllCommentsAsync, GetCommentByIdAsync) of the BlogManager class. The methods return lists of entities or single entities based on specified criteria.

Data Modification: The tests modify entities using the AddPostAsync, UpdatePostAsync, DeletePostAsync, AddCommentAsync, UpdateCommentAsync, and DeleteCommentAsync methods of the BlogManager class. They update entity properties, delete entities, and verify the success of the operations.

Assertions: The tests use assertions to verify the correctness of the CRUD operations. They ensure that data retrieved matches the expected results, that modifications are reflected correctly, and that appropriate exceptions are thrown when necessary.
