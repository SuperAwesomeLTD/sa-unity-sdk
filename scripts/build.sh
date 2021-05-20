############################################################ 
# step 1: download the iOS, Android & Unity source code
############################################################

./0.download.sources.sh

############################################################ 
# step 2: Build Android & move result to correct build folder
############################################################

./1.prepare.android.sh 

############################################################ 
# step 3: Build iOS & move result to correct build folder
############################################################

./2.prepare.ios.sh

############################################################ 
# step 4: Build Unity & move result to correct build folder
############################################################

./3.build.unity.sh