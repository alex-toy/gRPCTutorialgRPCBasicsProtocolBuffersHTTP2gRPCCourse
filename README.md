# gRPC Tutorial - gRPC Basics Protocol Buffers HTTP2 gRPC Course

gRPC is a modern open source high performance Remote Procedure Call (RPC) framework that can run in any environment. It can efficiently connect services in and across data centers with pluggable support for load balancing, tracing, health checking and authentication. It is also applicable in last mile of distributed computing to connect devices, mobile applications and browsers to backend services.

- gRPC is free and open-source framework developed by Google
- gRPC allows you to define REQUEST and RESPONSE for RPC (Remote Procedure Call) and handles the rest for you.
- It is Modern, Fast, Efficient, Low Latency
- Built on top of HTTP/2
- Supports streaming data
- Easy to implement authentication, load balancing, logging & monitoring

<img src="/pictures/grpc.png" title="grpc"  width="800">

## Create Project
<img src="/pictures/create_project.png" title="create project"  width="600">

- Right click on **GrpcService** and **Edit project file** :
<img src="/pictures/edit_project_file.png" title="edit project file"  width="600">

- Add the following lines :
```
<Protobuf Include="Protos\product.proto" GrpcServices="Server" />
<Protobuf Include="Protos\sample.proto" GrpcServices="Server" />
```

## Unary gRPC

<img src="/pictures/unary_grpc.png" title="unary grpc"  width="800">

### Install packages in GrpcClient
```
Grpc.Net.Client
Google.Protobuf
Grpc.Tools
```






https://www.youtube.com/watch?v=x-ktwMTN0Yw&list=PLzewa6pjbr3IOa6POjAMM0xiPZ-shjoem