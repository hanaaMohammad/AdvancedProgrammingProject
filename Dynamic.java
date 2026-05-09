

 

public class Dynamic {
private  int [] arr;
private  int Capacity;
private  int size;
   public Dynamic() {
this(100);
    }

   public Dynamic(int Capacity) {
    this.arr=new int [Capacity];
    this.Capacity=Capacity;
    this.size=0;
    }
public  void grwoUp(){
    if(this.Capacity==this.size){
        int [] Arry=new int [Capacity*2];
        this.Capacity=Capacity*2;
        int i=0;
for(int x :arr){
    Arry[i]=x;i++;
}
arr=Arry;

    }
}
public void add(int e){
    grwoUp();
    this.arr[size]=e;
    this.size++;
}
public int Size(){
    return size;
}
public void addFirst(int e){
grwoUp();
for(int i=size-1;i>=0;i--){
    arr[i+1]=arr[i];

}
arr[0]=e;
this.size++;

}
public void addInd(int e,int index){
    grwoUp();
    if(index>size||index<0)return;
    for(int i=size-1;i>=index;i--){
arr[i+1]=arr[i];
    }
    arr[index]=e;
    size++;
}
public int remove(){
    if(size<=0)return -1;
    int retuntE = arr[size-1];
    arr[size-1]=0;
    size--;
    return retuntE;
}
public int removeFirst(){
    int erturnEl=arr[0];
    if(size<=0)return -1;
    for(int i=0;i<size-1;i++){
        arr[i]=arr[i+1];

    }
    arr[size-1]=0;
    size--;
    return erturnEl;
}
public  int removeInd(int index){
    int ruternE=arr[index];
    if(index<0||size<=index||size==0)return -1;
    for(int i=index;i<size-1;i++){
        arr[i]=arr[i+1];
    }
    arr[size-1]=0;
    size--;
    return  ruternE;
}
    
}