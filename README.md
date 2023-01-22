# gRPC Tutorial - gRPC Basics Protocol Buffers HTTP2 gRPC Course

gRPC is a modern open source high performance Remote Procedure Call (RPC) framework that can run in any environment. It can efficiently connect services in and across data centers with pluggable support for load balancing, tracing, health checking and authentication. It is also applicable in last mile of distributed computing to connect devices, mobile applications and browsers to backend services.

- gRPC is free and open-source framework developed by Google
- gRPC allows you to define REQUEST and RESPONSE for RPC (Remote Procedure Call) and handles the rest for you.
- It is Modern, Fast, Efficient, Low Latency
- Built on top of HTTP/2
- Supports streaming data
- Easy to implement authentication, load balancing, logging & monitoring



## Create Project
<img src="/pictures/create_project.png" title="create project"  width="800">


## Producer project

1. Install **RabbitMQ Docker** image
```
docker run -d --hostname my-rabbit  --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

2. Go to http://localhost:15672 and give credentials guest / guest

<img src="/pictures/rabbitmq.png" title="rabbitmq on creation"  width="400">
<img src="/pictures/rabbitmq2.png" title="rabbitmq on creation"  width="800">
```

3. install packages
```
RabbitMQ.Client
```



https://www.youtube.com/watch?v=x-ktwMTN0Yw&list=PLzewa6pjbr3IOa6POjAMM0xiPZ-shjoem