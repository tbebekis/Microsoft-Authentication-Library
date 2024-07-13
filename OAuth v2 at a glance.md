# OAuth v2 at a glance

A summary of what OAuth is and the key terms related to it.

## OAuth

[OAuth (Open Authorization)](https://en.wikipedia.org/wiki/OAuth) is an [authorization](https://en.wikipedia.org/wiki/Authorization) specification for access delegation.

A User may grant access to his protected information stored in a website or service, to other services, applications and websites, without giving his credentials to those services or applications.

The authorized service or application acts **on behalf of** the User (delegation) in order to access the protected information. The User may specify the permissions the authorized service or application should have.

## OpenID Connect (OIDC)

[OpenID Connect (OIDC)](https://en.wikipedia.org/wiki/OpenID#OpenID_Connect_(OIDC)) is an [authentication](https://en.wikipedia.org/wiki/Authentication) specification built on top of OAuth v2.

Client applications use the OIDC protocol to request an ID token in order to authenticate a user. 

## Participants

**Participant** is a **term** used by OAuth in order to denote a person or system participating in the procedure that gives access to protected resources.

Following is the list of OAuth participants.

- **Resource Owner**. The User who has ownerhip of the protected resources.
- **Client**. A service or application requesting permissions to access the protected resources.
- **Authorization Server**. A service that authenticates the **Resource Owner** and, under his authorization, issues the **Access Token**.
- **Resource Server**. The service where the protected resources are stored.

## Terms

Besides **Participant** there is a number of other terms used by OAuth texts.

- **Resource or Asset**. Any protected information, such as texts, lists or images, stored in a server.
- **Client ID**. The identifier of a Client application, analogous to the `User ID` in user credentials.
- **Client Secret**. The password of a Client application, analogous to the `Password` in user credentials.
- **Public Client**. A client application **not** capable to maintain a **Client Secret** confidentially. A native desktop or mobile application running on a personal computer falls in this category.
- **Confidential Client**. A client application **capable** to maintain a **Client Secret** confidentially. An application running on a server, such as a service or a Web application falls in this category.
- **Token**. A [`JSON Web Token (JWT)`](https://en.wikipedia.org/wiki/JSON_Web_Token) which allows access to the protected resources.
- **Authorization Code**. An intermediate short-lived token used internally in some of OAuth flows.
- **Scope**. A way to limit the access a token is permitted to have to the protected resources. A Scope is a permission.
- **OAuth Flow**. A type of conversation between participants in order for the Client to  acquire a Token.
 

## Tokens

- **Access Token**. After proper authentication and authorization the **Authorization Server** issues an Access Token that is used by the **Client** in requesting **Resource Server** to allow access to its protected resources.
- **Refresh Token**. After a specified period of time an **Access Token** expires. A **Refresh Token** replaces the expired **Access Token** without requiring re-authentication of the **Resource Owner**.



## OAuth Flows

OAuth flows are types of conversations between participants in order for the Client to  acquire the Access Token. Each flow has its own steps.

OAuth v.2 uses **four** flows. 

- Authorization Code flow.
- Implicit flow.
- Resource Owner Password flow.
- Client Credentials flow.
 

## Authorization Code flow steps.

- The Resource Owner User clicks a login link in a Client Web Application.
- The Client Application sends a credential request to the Authorization Server.
- The Authorization Server redirects the Client Application to its login and authorization page.
- The Resource Owner User authenticates and may consent to a list of permissions the Authorization Server is going to give to the Client application.
- The Authorization Server issues a short-lived **Authorization Code** for the Client application.
- The Client application aquires an Access Token from the Authorization Server by exchanging that short-lived **Authorization Code**.
 

## Implicit flow steps.

- The Resource Owner User clicks a login link in a Client Web Application.
- The Client Application sends a credential request to the Authorization Server.
- The Authorization Server redirects the Client Application to its login and authorization page.
- The Resource Owner User authenticates and may consent to a list of permissions the Authorization Server is going to give to the Client application.
- The Authorization Server issues an Access Token and then redirects the Resource Owner User back to the Client application. 

> NOTE: This flow differs from `Authorization Code flow` in that it does not use an intermediate **Authorization Code**.

## Resource Owner Password flow steps.

- The Resource Owner User logs into a Client application using his credentials.
- The Client application forwards User credentials to Authorization Server.
- The Authorization Server validates the credentials and issues an Access Token. 

## Client Credentials flow steps.

 - The Authorization Server authenticates the Client application using the **Client ID** and **Client Secret**.
- The Authorization Server validates the credentials and issues an Access Token. 
  
> NOTE: This flow does **not** involve the Resource Owner User at all.


## Links
- [OAuth 2.0 Specification](https://datatracker.ietf.org/doc/html/rfc6749)
- [OAuth 2.0 Web Site](https://oauth.net/2/)
- [OAuth in Wikipedia](https://en.wikipedia.org/wiki/OAuth)
- [What Is OAuth?](https://frontegg.com/blog/oauth)
- [Diagrams And Movies Of All The OAuth 2.0 Flows](https://darutk.medium.com/diagrams-and-movies-of-all-the-oauth-2-0-flows-194f3c3ade85)
- [Which OAuth 2.0 Flow Should I Use?](https://auth0.com/docs/get-started/authentication-and-authorization-flow/which-oauth-2-0-flow-should-i-use)
- [JWT Authentication](https://frontegg.com/blog/jwt-authentication)
- [OAuth 2.0 and OIDC Glossary](https://docs.vindicia.com/bundle/b_ConnectProductDescription/page/topics/oAuth2OIDCGlossary_c.html)
 

