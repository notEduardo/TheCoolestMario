## Build Docker Image
docker build -t gcr.io/bg/thecoolestmario:v1 .
(TO RUN LOCALLY) docker run -p 8080:80 gcr.io/bg/thecoolesttaco:v1

## Get GCP Auth
gcloud auth configure-docker

## Push to GCP
docker push gcr.io/bg/thecoolestmario:v1

