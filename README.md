# project2-fakebook
## FAKEBOOK

A single-page clone of Facebook 
Should be similar to a blog where user can post a status and other users can post a response to them. Each user will have their own profile and won’t be able to post anything without signing in.     

## MVP
User authentication - login, logout, can’t make posts without an account
User authorization - can only edit your own profile / Public vs private posts that is viewable 
User profile - Personal info, uploadable profile picture image, status messages
Images - Can upload images with a caption
News Feed - Status updates of other people

## Possible Future Updates
Like function on pictures/statuses
Friend/unfriend requests
Comment on other user posts

## REST API
User Controller 
    CRUD operations for user account
Post Controller
    CRUD operations for posts
    Should be linked to user by ID

User Model
    ID (Unique)
    FirstName 
    Email/Username (Key)
    Password
    Profile Picture (optional)
    Bio (optional).
Post Model
    ID (Unique)
    Parent Post ID
    Content (string)
    Created at timestamp


## EXTRA NOTES
Page: (?)
    Message board    
## Creates posts (Kinds of posts)
    Status messages
    Likes(?)
    Upload pictures (any image type)
        Comment on user picture
## Newsfeed
    Filter by people you follow
    Is available to everyone (default).
    Upload pictures (any image type)
        Comment on user picture
