# CS408-Term Project

SUBook – a Facebook like Social Networking Application

project description:

In this project, you are going to develop a social networking application called SUBook (an
oversimplified version of Facebook) by implementing client and server modules. (i) The Server
module manages the storage of posts, posts feed, and friendships between the users, and (ii) the
Client module acts as a user who shares posts, adds and removes other users from his/her
friendship, and views posts of other users.
The server listens on a predefined port and accepts incoming client connections. There might
be one or more clients connected to the server at the same time. Each client knows the IP address
and the listening port of the server (to be entered through the Graphical User Interface (GUI)).
Clients connect to the server on a corresponding port and identify themselves with their usernames.
Server needs to keep the usernames of currently connected clients in order to avoid the same name
to be connected more than once at a given time to the server.
On the server side, there is a predefined database of users which are presumed to be
registered to the social network so that you do not need to implement any registration process
between a client and the server (actually it was done in HW1). That user database has been provided
to you in package with the term project as a textual file named user-db.txt together with this guide
document. You should parse this file in order to load the usernames in your server program; we
can use a different file in the demos, so please do not hardcode the usernames in your code. For
simplicity, a client, whose username is in the user database, will be able to connect by providing
his/her username only (i.e. no password or other type of security). Once connected, he/she will act
as mentioned in the rest of this document.
