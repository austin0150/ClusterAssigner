# ClusterAssigner
A small program to assign geographic X,Y coordinates to clusters

## This is a program that I built to aid in a project for my statistics class.

## When given a csv file named Records.csv contining data like this...  
 Date,Year,Month,Weekday,Hour,Primary Type,Latitude,Longitude,Location,State  
1/18/2016,2016,1,Monday,20,THEFT,41.839781079,-87.719547576,"(41.839781079, -87.719547576)",Illinois  
7/27/2015,2015,7,Monday,16,FRAUD,41.884516393,-87.619132695,"(41.884516393, -87.619132695)",Illinois  
3/14/2015,2015,3,Saturday,14,THEFT,41.919473537,-87.682573759,"(41.919473537, -87.682573759)",Illinois  
7/6/2015,2015,7,Monday,18,THEFT,41.951497795,-87.771829531,"(41.951497795, -87.771829531)",Illinois  
  
And a csv file named Clusters.csv containing data like this...  
41.909664252,-87.742728815,1  
41.979006297,-87.906463155,2  
41.883500187,-87.627876698,3  
  
The application will calculate which of the "clusters" that each of the records from the first file are closest to and assign them to the appropriate "cluster id" (1, 2, 3.. etc)  
The application will then output a copy of Records.csv with the "ClusterID" and "DistanceToCluster" columns added and populated.  
