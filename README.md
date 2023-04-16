# gRPC Tutorial - gRPC Basics Protocol Buffers HTTP2 gRPC Course

gRPC is a modern open source high performance Remote Procedure Call (RPC) framework that can run in any environment. It can efficiently connect services in and across data centers with pluggable support for load balancing, tracing, health checking and authentication. It is also applicable in last mile of distributed computing to connect devices, mobile applications and browsers to backend services.

- gRPC is free and open-source framework developed by Google
- gRPC allows you to define REQUEST and RESPONSE for RPC (Remote Procedure Call) and handles the rest for you.
- It is Modern, Fast, Efficient, Low Latency
- Built on top of HTTP/2
- Supports streaming data
- Easy to implement authentication, load balancing, logging & monitoring

<img src="/pictures/grpc.png" title="grpc"  width="800">

## Unary gRPC

<img src="/pictures/unary_grpc.png" title="unary grpc"  width="800">

### Create Projects GrpcClient and GrpcService
<img src="/pictures/create_project.png" title="create project"  width="600">

- Right click on **GrpcService** and **Edit project file** :
<img src="/pictures/edit_project_file.png" title="edit project file"  width="400">

- Add the following lines in **GrpcService**:
```
<ItemGroup>
    <Protobuf Include="Protos\product.proto" GrpcServices="Server" />
    <Protobuf Include="Protos\sample.proto" GrpcServices="Server" />
</ItemGroup>
```

- Do the same for **GrpcClient** and addd the following lines :
```
<ItemGroup>
    <Protobuf Include="Protos\product.proto" GrpcServices="Client" />
    <Protobuf Include="Protos\sample.proto" GrpcServices="Client" />
</ItemGroup>
```

### Install packages in GrpcClient
```
Grpc.Net.Client
Google.Protobuf
Grpc.Tools
```


## Server Streaming gRPC

gRPC Streaming APIs are the APIs with which we can stream data. Streaming data means we can send multiple or infinite data without any limitations using a single TCP connection. Streaming data is a highlighted benefit of gRPC APIs, as you know this option is not there in REST APIs. gRPC Streaming APIs are one of the best options if you are dealing with big data. There are three types of grpc streaming APIs.


1. **gRPC Server Streaming** [Server Streaming RPC]
A server-streaming RPC is like a unary RPC, except that the server returns a stream of messages in response to a client’s request. So, the client can send a single request to the server and the server will responds back with multiple multiple or a stream of messages.

<img src="/pictures/grpc_server_streaming.png" title="grpc server streaming"  width="800">

2. **gRPC Client Streaming** [Client Streaming RPC]
In a client-streaming RPC, the client will send multiple messages or a stream of messages to the server. But the server will respond back with a single message.

<img src="/pictures/grpc_client_streaming.png" title="grpc client streaming"  width="800">
<img src="/pictures/grpc_client_streaming_result.png" title="grpc client streaming result"  width="600">

3. **gRPC Bidirectional Streaming** [Bidirectional Streaming RPC]
In Bidirectional streaming RPC both client and server can send multiple messages. That means both request and response will be a stream of messages. Hence Bidirectional Streaming RPC is a two streaming API [C# gRPC two way Stream / DotNet gRPC two way Streaming].


