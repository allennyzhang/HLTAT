{
###Helix Leisure Technical Assessment Test (HLTAT)

1. Please use postman or anyother API testing tools to test getproduct and putproduct API.

###Project Basic Information

1. Project based on .Net Core 2.0 framework.

2. Project uses EF Core 2.0 and stores the data in memory database.

	(1). Use both DataAnnotation and Fluent API on data model.

3. Use following methods to enhance API performance.
	
	(1). Utilize Gzip response compression for all APIs.
	(2). Set the ResponseCache for retrieve data APIs.
	(3). Use Newtonsoft.Json serialize object to JSON which may not the fastest serialiser but recommended by Visual Studio.

4. Use Dependency Injection design pattern to inject DbContext and all project Services.

5. Please refer to following URL for Testing artefact.

}
