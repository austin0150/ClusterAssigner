# ClusterAssigner  
## Description  
This is a program that will take a csv file containing Longitude and Latitude, and add a column containing a cluster ID and distance to that cluster center, given a second csv file that contains coordinates of cluster centers.  
I built this program to help deal with a large dataset for a statistics class assignment.  

## It can help you take a large set of data and assign clusters to the datapoints. Then you can visualize with tools like Tableau.  
![CLUSTER MAP](Images/Cluster.png)  

## The program will take a provided csv file and add two columns, "Cluster ID" and "Distance to Cluster"  

## Prior to running the program 
* Have a csv file containing columns that include both longitude and latitude, that you want to assign a cluster to.
* Have a csv file that contains the cluster centers you want to use  

### This is an example of the CSV file you want to evaluate
 Date,Year,Month,Weekday,Hour,Primary Type,Latitude,Longitude,Location,State  
1/18/2016,2016,1,Monday,20,THEFT,41.839781079,-87.719547576,"(41.839781079, -87.719547576)",Illinois  
7/27/2015,2015,7,Monday,16,FRAUD,41.884516393,-87.619132695,"(41.884516393, -87.619132695)",Illinois  
3/14/2015,2015,3,Saturday,14,THEFT,41.919473537,-87.682573759,"(41.919473537, -87.682573759)",Illinois  
7/6/2015,2015,7,Monday,18,THEFT,41.951497795,-87.771829531,"(41.951497795, -87.771829531)",Illinois  
  
### This is an example of the CSV file that contains the cluster centers  
lat,long
41.909664252,-87.742728815  
41.979006297,-87.906463155  
41.883500187,-87.627876698  
  
## When Running the program  
* Choose the file to evaluate  
* Select the folder you want the results to be placed in  
* Enter the name of the new file you want to contain the results  
* The detected columns will be displayed, choose the column containing the Latitude  
* Choose the column that contains the Longitude  
* enter y or n to indicate if you want to set a maximun distance for each cluster (Points not in a cluster will be given a cluster id of -1)  
* if you selected to use a maximun distance, enter the maximum distance  
* Choose the CSV file that contains the cluster centers  
* The detected columns will be displayed, choose the column containing the Latitude  
* Choose the column containing the Longitude  
* Your new CSV file will be generated at the entered location  
  
### The application will calculate which of the "clusters" that each of the records from the first file are closest to and assign them to the appropriate "cluster id" (1, 2, 3.. etc)  
### The application will then output a copy of Records.csv with the "ClusterID" and "DistanceToCluster" columns added and populated.  
 