import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  allImages:[]=[];    
    
  getImages() {    
    return this.allImages == Imagesdelatils.slice(0);    
}    

getImage(id: number) {    
    return Imagesdelatils.slice(0).find(Images => Images.id == id)    
} 
}
const Imagesdelatils = [    
  { "id": 1, "brand": "Apple", "url": "src/assets/Images/Lumakin Data base schema.png" }    
  
] 
