When an HTTP client sends a request to a remote system the server can redirect the client to different URLs until he reaches the final system that delivers the actual application data. It can happen that parts of the system chain are not protected by HTTPS. With some effort an attacker can manipulate the client request to sniff sensitive data from the data stream.

Elk determines the system chain to the actual web server and caches the server header information. In a second step it analyzes the serverheaders and determines the weak spots in the chain.
