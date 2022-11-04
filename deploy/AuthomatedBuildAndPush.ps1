docker-compose -f docker-compose.yml -f docker-compose.production.yml up -d

docker tag identity dkrishen/mraidentity:host-1.2
docker tag gateway dkrishen/mragateway:host-1.2
docker tag rooms dkrishen/mrarooms:host-1.2
docker tag users dkrishen/mrausers:host-1.2
docker tag bookings dkrishen/mrabookings:host-1.2
docker tag angular dkrishen/mraspa:host-1.2.1
docker tag signalr dkrishen/mrasignalr:host-1.2

docker push dkrishen/mraidentity:host-1.2
docker push dkrishen/mragateway:host-1.2
docker push dkrishen/mrarooms:host-1.2
docker push dkrishen/mrausers:host-1.2
docker push dkrishen/mrabookings:host-1.2
docker push dkrishen/mraspa:host-1.2.1
docker push dkrishen/mrasignalr:host-1.2